CREATE TABLE [dbo].[PesoXTalla] (
    [Talla] INT             NULL,
    [-3]    DECIMAL (18, 1) NULL,
    [-2]    DECIMAL (18, 1) NULL,
    [-1]    DECIMAL (18, 1) NULL,
    [0]     DECIMAL (18, 1) NULL,
    [1]     DECIMAL (18, 1) NULL,
    [2]     DECIMAL (18, 1) NULL,
    [3]     DECIMAL (18, 1) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de referencia antropométrica que relaciona la talla (estatura) de un paciente con los rangos de peso esperado según desviaciones estándar (puntajes Z), usada para evaluar el estado nutricional y el crecimiento en pediatría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estatura del paciente en centímetros, usada como base para consultar los pesos de referencia según la talla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'Talla';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'Talla';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso mínimo extremo correspondiente a -3 desviaciones estándar (puntaje Z -3) para la talla indicada; indica desnutrición severa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'-3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'-3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso correspondiente a -2 desviaciones estándar (puntaje Z -2) para la talla indicada; límite inferior de peso normal, por debajo indica bajo peso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'-2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'-2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso correspondiente a -1 desviación estándar (puntaje Z -1) para la talla indicada; rango levemente por debajo de la mediana.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'-1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'-1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso mediano o promedio esperado (puntaje Z 0) para la talla indicada; representa el valor de referencia central del crecimiento normal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'0';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'0';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso correspondiente a +1 desviación estándar (puntaje Z +1) para la talla indicada; rango levemente por encima de la mediana.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso correspondiente a +2 desviaciones estándar (puntaje Z +2) para la talla indicada; límite superior de peso normal, por encima indica sobrepeso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso máximo extremo correspondiente a +3 desviaciones estándar (puntaje Z +3) para la talla indicada; indica obesidad severa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PesoXTalla', @level2type = N'COLUMN', @level2name = N'3';
