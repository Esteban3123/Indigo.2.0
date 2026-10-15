CREATE TABLE [Billing].[BotServicesProceduresLog] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Row]            INT            NOT NULL,
    [DataSourceType] INT            NOT NULL,
    [State]          BIT            NOT NULL,
    [Message]        NVARCHAR (MAX) NULL,
    [CreationDate]   DATETIME       NOT NULL,
    [UpdateDate]     DATETIME       NULL,
    [InitialDate]    DATETIME       NULL,
    [EndDate]        DATETIME       NULL,
    [Data]           NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_BotServicesProceduresLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_BotServicesProceduresLog_DataSourceType_EndDate_InitialDate_CreationDate_Data_Message_Row_State_UpdateDate]
    ON [Billing].[BotServicesProceduresLog]([DataSourceType] ASC, [EndDate] ASC, [InitialDate] ASC)
    INCLUDE([CreationDate], [Data], [Message], [Row], [State], [UpdateDate]);


GO
CREATE NONCLUSTERED INDEX [IX_BotServicesProceduresLog_DataSourceType_EndDate_InitialDate_Row]
    ON [Billing].[BotServicesProceduresLog]([DataSourceType] ASC, [EndDate] ASC, [InitialDate] ASC, [Row] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el JSON con la información y el mensaje del proceso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Data';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'UpdateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha actual del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina el estado del registro:  0 - Cuando falle  1 - Cuando se haya ejecutado sin problema', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Laboratorios  2 - ImagenesDx  3 - Patologías  4 - ProcedimientosNoQx  5 - Interconsultas  6 - Terapias  7 - Procedimientos de Enfermeria  8 - Consumo de Oxigeno  9 - Valoraciones  10- central de mezclas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'DataSourceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Auto que determina el registro de la tabla HCORDLABO', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Row';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BotServicesProceduresLog', @level2type = N'COLUMN', @level2name = N'Id';

