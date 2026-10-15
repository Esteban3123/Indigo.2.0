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
ALTER TABLE [dbo].[MedicationAuthorization] NOCHECK CONSTRAINT [FK_SEGusuaru_CancellationUser];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion de la anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'CancellationJustification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha cancelacion  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Autorizacion de medicamentos:   1-Solicitad     (Aun no esta programado esto 21-10-2022)  2-Autorizado     (Aun no esta programado esto 21-10-2022)  3-Anulado   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la Tabla HCPRESCRA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'IdHCPRESCRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MedicationAuthorization', @level2type = N'COLUMN', @level2name = N'Id';

