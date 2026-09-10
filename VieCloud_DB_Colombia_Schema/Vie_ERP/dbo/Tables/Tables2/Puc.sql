CREATE TABLE [dbo].[Puc] (
    [Libro]              FLOAT (53)     NULL,
    [nivel]              FLOAT (53)     NULL,
    [clase]              FLOAT (53)     NULL,
    [Cuenta]             NVARCHAR (255) NULL,
    [Naturaleza]         FLOAT (53)     NULL,
    [Nomb]               NVARCHAR (255) NULL,
    [Padre]              NVARCHAR (255) NULL,
    [Manej Terce#]       NVARCHAR (255) NULL,
    [Cierre Tercero]     NVARCHAR (255) NULL,
    [Nit tercero- VACIA] NVARCHAR (255) NULL,
    [Permite conciliar]  NVARCHAR (255) NULL,
    [Dispo- Cte No Cte#] NVARCHAR (255) NULL,
    [C#Cost]             NVARCHAR (255) NULL,
    [Tipo Retencion]     NVARCHAR (255) NULL,
    [Categoria]          NVARCHAR (255) NULL,
    [Permite Mov]        NVARCHAR (255) NULL,
    [Status]             NVARCHAR (255) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla que almacena el Plan Único de Cuentas (PUC) contable, estructurado jerárquicamente mediante niveles, clases y cuenta padre. Registra atributos contables como naturaleza de la cuenta, manejo de terceros, tipo de retención, centro de costos y si permite movimientos o conciliación, propios de la parametrización contable en sistemas ERP del sector salud colombiano.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Puc';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Puc';
GO
