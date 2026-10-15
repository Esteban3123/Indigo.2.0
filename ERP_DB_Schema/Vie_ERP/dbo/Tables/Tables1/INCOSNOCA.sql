CREATE TABLE [dbo].[INCOSNOCA] (
    [CODCONOCA]  CHAR (3)     NOT NULL,
    [DESCONOCA]  CHAR (40)    NOT NULL,
    [TIPCOSNOCA] CHAR (1)     NULL,
    [INDAUDFOR]  NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_INCOSNOCA] PRIMARY KEY CLUSTERED ([CODCONOCA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipos de Costos de No Calidad  1: Estancia  2: Medicamentos e Insumos  3: Procedimientos o Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'TIPCOSNOCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Costos de No Calidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'DESCONOCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Costos de No Calidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'CODCONOCA';

