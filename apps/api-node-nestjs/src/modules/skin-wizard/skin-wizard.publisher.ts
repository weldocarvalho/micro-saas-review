import { Injectable, OnModuleInit, OnModuleDestroy } from '@nestjs/common';
import amqp, { AmqpConnectionManager, ChannelWrapper } from 'amqp-connection-manager';
import { MassTransitEnvelope, SkinWizardSubmittedEvent } from './skin-wizard.contracts';

@Injectable()
export class SkinWizardPublisher implements OnModuleInit, OnModuleDestroy {
  private connection!: AmqpConnectionManager;
  private channelWrapper!: ChannelWrapper;

  // OnModuleInit roda automaticamente quando o NestJS liga
  async onModuleInit() {

  this.connection = amqp.connect(['amqp://localhost:5672']);

  this.channelWrapper = this.connection.createChannel({
    json: true,
    setup: (channel: any) => {
      // AJUSTE AQUI: Nome completo da exchange esperada pelo MassTransit
      return channel.assertExchange(
        'DermePlan.Worker.Application.Models:SkinWizardSubmittedEvent', 
        'fanout', 
        { durable: true }
      );
    },
  });
}

  async publishSubmission(data: SkinWizardSubmittedEvent): Promise<void> {
    
  const envelope: MassTransitEnvelope<SkinWizardSubmittedEvent> = {
    message: data,
    messageType: ['urn:message:DermePlan.Worker.Application.Models:SkinWizardSubmittedEvent'],
  };

  // AJUSTE AQUI: Publica na Exchange correta com o prefixo do namespace
  await this.channelWrapper.publish(
    'DermePlan.Worker.Application.Models:SkinWizardSubmittedEvent',
    '', // Sem routing key (Fanout ignora isso)
    envelope,
    { contentType: 'application/vnd.masstransit+json' } 
  );
}


  // Fecha as conexões de forma segura se o NestJS desligar (Graceful Shutdown)
  async onModuleDestroy() {
    await this.channelWrapper.close();
    await this.connection.close();
  }
}
