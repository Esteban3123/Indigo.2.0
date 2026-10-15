CREATE TABLE [dbo].[PADORDENTERAPIAS] (
    [ID]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADCONTROL]             INT           NOT NULL,
    [CODSERIPS]                CHAR (20)     NOT NULL,
    [CANTIDAD]                 INT           NOT NULL,
    [DURACION]                 INT           NOT NULL,
    [UNIDADMEDIDA]             INT           NOT NULL,
    [MOTIVO]                   VARCHAR (MAX) NULL,
    [IDDESCRIPCIONRELACIONADA] INT           NULL,
    CONSTRAINT [PK_PHDORDTERAPIAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PHDORDTERAPIAS_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_PHDORDTERAPIAS_PHDCONTROL] FOREIGN KEY ([IDPADCONTROL]) REFERENCES [dbo].[PADCONTROL] ([ID])
);


GO
ALTER TABLE [dbo].[PADORDENTERAPIAS] NOCHECK CONSTRAINT [FK_PHDORDTERAPIAS_INCUPSIPS];


GO
ALTER TABLE [dbo].[PADORDENTERAPIAS] NOCHECK CONSTRAINT [FK_PHDORDTERAPIAS_PHDCONTROL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - CUPS interconsulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo observacion de terapias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'MOTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'undiad de medidad de la cantidad de duracion: 5 terapias durante 2 meses.    1 - Dias  2 - Meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'UNIDADMEDIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cantidad de duracion de la terapia ej:    10 sesiones de terapia durante 30 dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'DURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cantidad de terapias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de servicio de terapias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id con la tabla cabecera de control PHD (PHDCONTROL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'IDPADCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'ID';

