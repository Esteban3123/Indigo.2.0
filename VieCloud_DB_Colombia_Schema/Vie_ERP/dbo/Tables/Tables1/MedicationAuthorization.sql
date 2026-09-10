CREATE TABLE [dbo].[MedicationAuthorization] (
    [Id]                        INT           IDENTITY (1, 1) NOT NULL,
    [IdHCPRESCRA]               INT           NOT NULL,
    [Status]                    INT           NOT NULL,
    [UserCode]                  CHAR (20)     NOT NULL,
    [Date]                      DATETIME      NOT NULL,
    [CancellationJustification] VARCHAR (250) NULL,
    CONSTRAINT [PK_MedicationAuthorization] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HCPRESCRA_IdHCPRESCRA] FOREIGN KEY ([IdHCPRESCRA]) REFERENCES [dbo].[HCPRESCRA] ([ID]),
    CONSTRAINT [FK_SEGusuaru_CancellationUser] FOREIGN KEY ([UserCode]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[MedicationAuthorization] NOCHECK CONSTRAINT [FK_HCPRESCRA_IdHCPRESCRA];




GO
ALTER TABLE [dbo].[MedicationAuthorization] NOCHECK CONSTRAINT [FK_HCPRESCRA_IdHCPRESCRA];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación o motivo de la anulación de la autorización de medicamento. Campo de texto libre (VARCHAR 250) que registra la razón por la cual se cancela la prescripción autorizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'CancellationJustification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion de la anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'CancellationJustification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'CancellationJustification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la cancelación o registro de la autorización de medicamento. Tipo DATETIME que captura cuándo se procesó la anulación o cambio de estado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha cancelacion  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario (profesional de salud o administrativo) que realizó la cancelación de la autorización. Referencia a tabla SEGusuaru, vinculada con el campo CODUSUARI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la autorización de medicamento: 1=Solicitado (pendiente de autorización), 2=Autorizado (medicamento aprobado), 3=Anulado (prescripción cancelada). Tipo INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Autorizacion de medicamentos:   1-Solicitad     (Aun no esta programado esto 21-10-2022)  2-Autorizado     (Aun no esta programado esto 21-10-2022)  3-Anulado   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la prescripción (receta) de medicamento en la tabla HCPRESCRA. Vinculo de clave foránea que relaciona la autorización con el registro clínico de prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'IdHCPRESCRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la Tabla HCPRESCRA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'IdHCPRESCRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'IdHCPRESCRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (primary key) del registro de autorización de medicamento. Tipo INT con auto-incremento (IDENTITY 1,1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de autorizaciones de medicamentos prescritos. Guarda el estado de aprobación o cancelación de cada prescripción médica, incluyendo el usuario que gestionó la autorización, la fecha de la acción y la justificación en caso de cancelación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization';
