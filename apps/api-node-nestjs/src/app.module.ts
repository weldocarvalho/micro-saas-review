// src/app.module.ts
import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { SkinWizardModule } from './modules/skin-wizard/skin-wizard.module';

@Module({
  imports: [
    SkinWizardModule // Importa o módulo do Wizard para ativar suas rotas e injeções
  ], 
  controllers: [
    AppController    // Mantém o controller de Healthcheck ativo na raiz
  ],
  providers: [],     // Deixamos vazio já que limpamos o AppService padrão do Nest
})
export class AppModule {}
