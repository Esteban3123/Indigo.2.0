CREATE TABLE [dbo].[INTIMAGEN] (
    [AUTO]         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ORDEN_INDIGO] VARCHAR (20)    NOT NULL,
    [CODSERIPS]    VARCHAR (15)    NOT NULL,
    [IMAGENLAB]    VARBINARY (MAX) NOT NULL,
    [DESIMGLAB]    VARCHAR (50)    NULL,
    [FECREGSIS]    DATETIME        NULL,
    CONSTRAINT [PK_INTIMAGEN] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_INTIMAGEN__ORDEN_INDIGO__CODSERIPS__INC__DESIMGLAB__IMAGENLAB]
    ON [dbo].[INTIMAGEN]([ORDEN_INDIGO] ASC, [CODSERIPS] ASC)
    INCLUDE([DESIMGLAB], [IMAGENLAB]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro en el Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Adicional de la Imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'DESIMGLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imagen respectiva al resultado del laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'IMAGENLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Orden Indigo  Paciente concatenado con el numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'AutoNumerico Identity', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'AUTO';

