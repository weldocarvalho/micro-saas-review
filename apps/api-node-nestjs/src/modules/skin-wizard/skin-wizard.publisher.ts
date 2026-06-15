import { Injectable, OnModuleInit, OnModuleDestroy } from '@nestjs/common';
import amqp, { AmqpConnectionManager, ChannelWrapper } from 'amqp-connection-manager';
import { MassTransitEnvelope, SkinWizardSubmittedEvent } from './skin-wizard.contracts';

@Injectable()
export class SkinWizardPublisher implements OnModuleInit, OnModuleDestroy {
  private connection!: AmqpConnectionManager;
  private channelWrapper!: ChannelWrapper;

  // OnModuleInit roda automaticamente quando o NestJS liga
  async onModuleInit() {
    // Conecta no seu RabbitMQ local
    this.connection = amqp.connect(['amqp://localhost:5672']);

    this.channelWrapper = this.connection.createChannel({
      json: true,
      setup: (channel: any) => {
        // O MassTransit em C# cria Exchanges do tipo 'fanout' com o nome exato do contrato/evento
        return channel.assertExchange('SkinWizardSubmittedEvent', 'fanout', { durable: true });
      },
    });
  }

  async publishSubmission(data: SkinWizardSubmittedEvent): Promise<void> {
    const envelope: MassTransitEnvelope<SkinWizardSubmittedEvent> = {
      message: data,
      // Altere o namespace abaixo ("MeuSaaS.Contratos") para bater EXATAMENTE com o Namespace do seu record/class no C#
      messageType: ['urn:message:MeuSaaS.Contratos:SkinWizardSubmittedEvent'],
    };

    // Publica na Exchange do RabbitMQ de forma assíncrona
    await this.channelWrapper.publish(
      'SkinWizardSubmittedEvent',
      '', // Sem routing key (Fanout ignora isso)
      envelope,
      { contentType: 'application/vnd.masstransit+json' } // Formato obrigatório do MassTransit
    );
  }

  // Fecha as conexões de forma segura se o NestJS desligar (Graceful Shutdown)
  async onModuleDestroy() {
    await this.channelWrapper.close();
    await this.connection.close();
  }
}
