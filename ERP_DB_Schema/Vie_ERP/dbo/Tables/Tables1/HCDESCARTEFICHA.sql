CREATE TABLE [dbo].[HCDESCARTEFICHA] (
    [ID]     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO] VARCHAR (3)  NOT NULL,
    [NOMBRE] VARCHAR (50) NOT NULL,
    [ESTADO] BIT          NOT NULL,
    CONSTRAINT [PK_HCDESCARTEFICHA] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'True = Activo   False = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDESCARTEFICHA', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre o Descripcion del registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDESCARTEFICHA', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDESCARTEFICHA', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDESCARTEFICHA', @level2type = N'COLUMN', @level2name = N'ID';

