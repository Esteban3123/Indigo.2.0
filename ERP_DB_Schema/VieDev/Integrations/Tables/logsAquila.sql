CREATE TABLE [Integrations].[logsAquila] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [CreationDate] DATETIME       NOT NULL,
    [Transmitter]  VARCHAR (50)   NOT NULL,
    [Action]       VARCHAR (50)   NOT NULL,
    [MessajeJSON]  NVARCHAR (MAX) NULL,
    [LogMessage]   VARCHAR (MAX)  NULL,
    [Entity]       VARCHAR (50)   NULL,
    [IdOrder]      VARCHAR (50)   NULL,
    CONSTRAINT [PK_logsAquila] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'IdOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla en base de datos (AMBORDIMA-HCORDIMAG)', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'Entity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje Obtenido', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'LogMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON del mensaje ', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'MessajeJSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la accion.

VieSendOrder
VieCancelOrder
VieUpdatePatient

AquilaResult
AquilaAdemdum
AquilaCancelOrder
AquilaChangeStatus', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'Action';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indigo - Aquila', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'Transmitter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'CreationDate';

