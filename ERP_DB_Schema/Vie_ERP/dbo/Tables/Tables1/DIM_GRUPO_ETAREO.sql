CREATE TABLE [dbo].[DIM_GRUPO_ETAREO] (
    [ID_GRUPO_ETAREO]          INT            IDENTITY (0, 1) NOT FOR REPLICATION NOT NULL,
    [GRUPO_ETAREO_EDAD]        FLOAT (53)     NOT NULL,
    [GRUPO_ETAREO_RES_5268]    NVARCHAR (100) NOT NULL,
    [GRUPO_ETAREO_UPC]         NVARCHAR (100) NULL,
    [GRUPO_ETAREO_CICLO_VITAL] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_GRUPO_ETAREO] PRIMARY KEY CLUSTERED ([ID_GRUPO_ETAREO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ciclo vital', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_CICLO_VITAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la unidad de pago por capacitación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_UPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la resolución 5268', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_RES_5268';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'ID_GRUPO_ETAREO';

