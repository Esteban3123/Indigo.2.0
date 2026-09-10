CREATE TABLE [MedicalHistory].[TraceabilityDrugs] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [ProductCode]         CHAR (20)     NOT NULL,
    [NUMINGRES]           CHAR (10)     NOT NULL,
    [ProfessionalCode]    CHAR (20)     NOT NULL,
    [RegistrationDate]    DATETIME      NOT NULL,
    [Action]              INT           NOT NULL,
    [UFUCODIGO]           CHAR (10)     NOT NULL,
    [AdministrationRoute] VARCHAR (250) NULL,
    [Justification]       VARCHAR (250) NULL,
    [IdSourceTable]       INT           NOT NULL,
    [SourceTable]         VARCHAR (50)  NOT NULL,
    [ProccesType]         TINYINT       CONSTRAINT [DF_TraceabilityDrugs_ProccesType] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_TraceabilityDrugs] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de proceso farmacológico (TINYINT): 1=Prescripción de medicamentos, 2=Mezclas y líquidos. Clasifica el origen del movimiento de fármaco en la trazabilidad.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'ProccesType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Precipcion de medicamentos
2 - Mezclas y liquidos', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'ProccesType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'ProccesType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de acción ejecutada (INT 1-19): operaciones de enfermería (programación, adición, modificación, aplicación, eliminación, no aplicado, dosis adicional, suspensión, descarte), solicitud de insumos, gestión hospitalaria (traslado, aceptación, rechazo, devolutivo) y farmacia (entrega, anulación, devolución). Registra cada paso del ciclo de vida del medicamento.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Proceso que realiza la acción:    1 - Enfermeria hoja de medicamento - Programacion Medicamento.  2 - Enfermeria hoja de medicamento - Adicion de medicamento Medicamento.  3 - Enfermeria hoja de medicamento - Modificacion de medicamento.  4 -  Enfermeria hoja de medicamento - Aplicacion de Medicamento.  5 -  Enfermeria hoja de medicamento - Eliminar Medicamento.  6 -  Enfermeria hoja de medicamento - NO Aplicado de Medicamento.  7 -  Enfermeria hoja de medicamento - Dosis Adicional Medicamento.  8 -  Enfermeria hoja de medicamento - Suspender Aplicaciones.  9 -  Enfermeria hoja de medicamento - Descartar sobrante  10 -  Enfermeria hoja de medicamento - Aplicacion segun necesidad.  11 - Enfermeria solicitud medicamento insumos - Solicitud de enfermeria.  12- Gestion hospitalaria - Traslado de medicamento  13- Gestion hospitalaria - Aceptacion de traslado de medicamento.  14- Gestion hospitalaria - Rechazo de  traslado de medicamento.  15- Gestion hospitalaria - Devolutivo de medicamento.  16 - Farmacia - Entrega de medicamento.  17 - Farmacia - Anulacion de solicitud de medicamento.  18 - Farmacia - Aceptacion de devolutivo.  19 - Farmacia - Anulacion de devolutivo.  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'Action';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (DATETIME) del movimiento o transacción del medicamento. Fecha y hora exacta de registro del evento en la trazabilidad farmacológica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro del movimiento realizado', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'RegistrationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (CHAR 20) del profesional de salud (enfermero, farmacéutico) que ejecuta la acción. Código del personal responsable del movimiento.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo profesional que efectua la accion ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o admisión del paciente (CHAR 10). Identifica el episodio de atención/hospitalización asociado al movimiento de medicamento.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Ingreso', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento o producto farmacológico (CHAR 20). Identificador único del fármaco, insumo o preparación en trazabilidad.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'ProductCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Producto ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'ProductCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'ProductCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de cada registro de movimiento. Clave primaria de trazabilidad de medicamentos.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Trazabilidad de medicamentos administrados o gestionados durante un ingreso: registra cada acción realizada sobre un producto farmacéutico (dispensación, devolución, anulación, etc.), quién la ejecutó, cuándo y desde qué módulo del sistema.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (servicio o área hospitalaria) donde se realizó la acción sobre el medicamento, por ejemplo urgencias, hospitalización o farmacia.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de administración del medicamento, por ejemplo oral, intravenosa, intramuscular o sublingual.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'AdministrationRoute';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'AdministrationRoute';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación o motivo registrado por el profesional al momento de ejecutar la acción sobre el medicamento, como una sustitución, devolución o anulación.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro origen en la tabla fuente desde la cual se generó este evento de trazabilidad.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'IdSourceTable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'IdSourceTable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la tabla o módulo del sistema que originó el registro, indicando el proceso desde el cual se disparó la trazabilidad (por ejemplo, dispensación, receta u orden médica).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'SourceTable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TraceabilityDrugs', @level2type = N'COLUMN', @level2name = N'SourceTable';
