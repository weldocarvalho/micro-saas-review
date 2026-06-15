import { Injectable } from '@nestjs/common';
import { SkinWizardPublisher } from './skin-wizard.publisher';
import { SkinWizardSubmittedEvent } from './skin-wizard.contracts';

@Injectable()
export class SkinWizardService {
  // Injeta o nosso publicador de RabbitMQ de forma nativa via construtor (igualzinho ao C#)
  constructor(private readonly publisher: SkinWizardPublisher) {}

  async processWizardSubmission(dto: SkinWizardSubmittedEvent) {
    // PASSO 1: Aqui futuramente entrará a chamada do Drizzle ORM para salvar no PostgreSQL:
    // await this.db.insert(wizardSubmissions).values({...})
    console.log(`[WizardService] Salvando respostas do usuário ${dto.userId} no banco...`);

    // PASSO 2: Envia o evento para a fila do RabbitMQ
    console.log(`[WizardService] Despachando evento para o Worker .NET processar via IA...`);
    await this.publisher.publishSubmission(dto);

    return {
      success: true,
      status: 'PROCESSING',
      message: 'Questionário recebido! Nossa IA está analisando sua pele.'
    };
  }
}
