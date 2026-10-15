CREATE TABLE [dbo].[HKADENDUM] (
    [ID]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [numero_solicitud]        INT           NULL,
    [codigo_interno_servicio] INT           NULL,
    [cups]                    VARCHAR (20)  NULL,
    [item]                    INT           NULL,
    [fecha]                   DATE          NULL,
    [hora]                    TIME (7)      NULL,
    [resultados]              VARCHAR (MAX) NULL,
    [codigo_radiologo]        VARCHAR (20)  NULL,
    CONSTRAINT [PK_HKADEMDUM] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de usuario ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'codigo_radiologo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Adendum', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'resultados';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de envio  HH:MM:SS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'hora';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de envio YYYY-MM-DD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'fecha';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo que nos indica cuantos adendum tiene una solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'item';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Códificacion única de procedimientos en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'cups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código interno del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador y  código único de solictud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'numero_solicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autoincrementable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'ID';

