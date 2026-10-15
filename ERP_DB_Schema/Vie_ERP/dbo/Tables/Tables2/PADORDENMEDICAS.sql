CREATE TABLE [dbo].[PADORDENMEDICAS] (
    [ID]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADCONTROL] INT           NOT NULL,
    [CODPRODUC]    CHAR (20)     NOT NULL,
    [CANTIDAD]     INT           NOT NULL,
    [CODVIAADM]    VARCHAR (20)  NOT NULL,
    [FRECUENCIA]   VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_PHCORDMEDICA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PHCORDMEDICA_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_PHCORDMEDICA_PHDCONTROL] FOREIGN KEY ([IDPADCONTROL]) REFERENCES [dbo].[PADCONTROL] ([ID]),
    CONSTRAINT [FK_PHDORDENMEDICAS_HCVIAADMI] FOREIGN KEY ([CODVIAADM]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM])
);


GO
ALTER TABLE [dbo].[PADORDENMEDICAS] NOCHECK CONSTRAINT [FK_PHCORDMEDICA_IHLISTPRO];


GO
ALTER TABLE [dbo].[PADORDENMEDICAS] NOCHECK CONSTRAINT [FK_PHCORDMEDICA_PHDCONTROL];


GO
ALTER TABLE [dbo].[PADORDENMEDICAS] NOCHECK CONSTRAINT [FK_PHDORDENMEDICAS_HCVIAADMI];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de frecuencia medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'FRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Via de administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cantidad de producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo del producto IHLISTPRO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID relacion tabla control de PHD (PHDCONTROL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'IDPADCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'ID';

