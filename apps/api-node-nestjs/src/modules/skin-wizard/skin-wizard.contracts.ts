// O contrato de dados exato que o seu C# espera receber
// export class SkinWizardSubmittedEvent {
//   userId!: string;
//   skinType!: string;      // ex: 'oleosa', 'seca', 'mista'
//   celluliteStage!: string; // ex: 'grau-1', 'grau-2'
//   habitsDescription! : string;
// }

export interface SkinWizardSubmittedEvent {
  userId: string;
  skinType: string;      // ex: 'oleosa', 'seca', 'mista'
  celluliteStage: string; // ex: 'grau-1', 'grau-2'
  habitsDescription: string;
}

// O envelope padrão que o MassTransit exige por baixo dos panos
export interface MassTransitEnvelope<T> {
  message: T;
  messageType: string[]; // Indica para o .NET qual classe C# mapear
}
