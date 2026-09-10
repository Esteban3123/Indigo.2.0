CREATE TABLE [dbo].[SubirHom] (
    [CuentColgap]  FLOAT (53) NULL,
    [CuentaColgap] FLOAT (53) NULL,
    [idNiif]       FLOAT (53) NULL,
    [CuentaNiif]   FLOAT (53) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de trabajo/staging que almacena correspondencias entre cuentas contables, relacionando códigos y números de cuenta bajo dos esquemas: el plan de cuentas local (Colgap) y el estándar internacional (NIIF). Probablemente se usa en un proceso de homologación o migración contable entre ambos marcos normativos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SubirHom';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SubirHom';
GO
