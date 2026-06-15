import { Module } from '@nestjs/common';
import { SkinWizardController } from './skin-wizard.controller';
import { SkinWizardService } from './skin-wizard.service';
import { SkinWizardPublisher } from './skin-wizard.publisher';

@Module({
  controllers: [SkinWizardController],
  providers: [SkinWizardService, SkinWizardPublisher],
})
export class SkinWizardModule {}
