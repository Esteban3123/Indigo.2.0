CREATE TABLE [dbo].[HCRADCONTORNEO] (
    [ID]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADORDEN]       INT           NOT NULL,
    [CODCENATECONTORNEO] CHAR (10)     NULL,
    [OBSERVACION]        VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCRADCONTORNEO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADCONTORNEO_ADCENATEN] FOREIGN KEY ([CODCENATECONTORNEO]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADCONTORNEO_HCRADORD] FOREIGN KEY ([IDHCRADORDEN]) REFERENCES [dbo].[HCRADORDEN] ([ID])
);


GO
ALTER TABLE [dbo].[HCRADCONTORNEO] NOCHECK CONSTRAINT [FK_HCRADCONTORNEO_ADCENATEN];


GO
ALTER TABLE [dbo].[HCRADCONTORNEO] NOCHECK CONSTRAINT [FK_HCRADCONTORNEO_HCRADORD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la observación ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCONTORNEO', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de Atencion que seleccionar al crear el contorneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCONTORNEO', @level2type = N'COLUMN', @level2name = N'CODCENATECONTORNEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id de la orden medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCONTORNEO', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADCONTORNEO', @level2type = N'COLUMN', @level2name = N'ID';

