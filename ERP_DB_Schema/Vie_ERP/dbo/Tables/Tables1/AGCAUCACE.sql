CREATE TABLE [dbo].[AGCAUCACE] (
    [CODCAUCAN] CHAR (3)       NOT NULL,
    [DESCAUCAN] NVARCHAR (150) NOT NULL,
    [ESTCAUCAN] BIT            NOT NULL,
    CONSTRAINT [PK_AGCAUCACE] PRIMARY KEY CLUSTERED ([CODCAUCAN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado(Activo o inactivo) de la Causa de cancelación de la cita en espera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCACE', @level2type = N'COLUMN', @level2name = N'ESTCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de Causa de cancelacion de las citas en espera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCACE', @level2type = N'COLUMN', @level2name = N'DESCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Causa de Cancelacion de la citas en espera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCACE', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';

