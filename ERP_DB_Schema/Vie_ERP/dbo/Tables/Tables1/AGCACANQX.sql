CREATE TABLE [dbo].[AGCACANQX] (
    [CODCACANQ]   CHAR (3)       NOT NULL,
    [DESCAUCAN]   NVARCHAR (150) NOT NULL,
    [ESTCAUCAN]   BIT            NOT NULL,
    [CANCELARWEB] BIT            CONSTRAINT [DF__AGCACANQX__CANCE__0D36447B] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AGCACANQX] PRIMARY KEY CLUSTERED ([CODCACANQ] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usar en cancelacion de citas WEB --> 0: No , 1: Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'CANCELARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado(Activo o inactivo) de la Causa de cancelación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'ESTCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de Causa de cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'DESCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Causa de Cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'CODCACANQ';

