CREATE TABLE [Billing].[AdmissionsLiquidateStayDay] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [AdmissionNumber] VARCHAR (10) NOT NULL,
    [CreationDate]    DATETIME     NOT NULL,
    [UpdateDate]      DATETIME     NOT NULL,
    CONSTRAINT [PK_AdmissionsLiquidateStayDay] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha de la última modificación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AdmissionsLiquidateStayDay', @level2type = N'COLUMN', @level2name = N'UpdateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha de creación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AdmissionsLiquidateStayDay', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AdmissionsLiquidateStayDay', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AdmissionsLiquidateStayDay', @level2type = N'COLUMN', @level2name = N'Id';

