import { Controller, Post, Body, HttpCode, HttpStatus } from '@nestjs/common';
import { SkinWizardService } from './skin-wizard.service';
import type { SkinWizardSubmittedEvent } from './skin-wizard.contracts';
//import { SkinWizardSubmittedEvent } from './skin-wizard.contracts';


@Controller('wizard') // Rota base: http://localhost:3333/wizard
export class SkinWizardController {
  constructor(private readonly wizardService: SkinWizardService) {}

  @Post('submit') // Sub-rota: POST http://localhost:3333/wizard/submit
  @HttpCode(HttpStatus.OK) // Retorna HTTP 200 ao invés do padrão 201 do Nest
  async submitWizard(@Body() body: SkinWizardSubmittedEvent) {
    // O @Body joga o JSON recebido direto na variável 'body'
    return await this.wizardService.processWizardSubmission(body);
  }
}
