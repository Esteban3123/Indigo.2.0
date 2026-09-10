CREATE TABLE [dbo].[HCPARCONO] (
    [CODVIAADM]                CHAR (3)        NOT NULL,
    [DESVIAADM]                CHAR (50)       NOT NULL,
    [RANGMINIM]                DECIMAL (18, 2) NULL,
    [RANGMAXIM]                DECIMAL (18, 2) NULL,
    [ESTADO]                   BIT             NOT NULL,
    [CODSERIPS]                CHAR (20)       NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT             NULL,
    [GasType]                  INT             NULL,
    CONSTRAINT [PK_HCPARCONO] PRIMARY KEY CLUSTERED ([CODVIAADM] ASC),
    CONSTRAINT [FK_HCPARCONO_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_HCPARCONO]
    ON [dbo].[HCPARCONO]([CODVIAADM] ASC, [CODSERIPS] ASC, [IDDESCRIPCIONRELACIONADA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de gas medicinal: 1=Oxígeno, 2=Óxido Nítrico, 3=Oxihelio. INT, clasificación para administración terapéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'GasType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que almacena el  Tipo de gas    1 - Oxigeno  2 - Oxido Nititico  3 - Oxihelio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'GasType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'GasType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de descripción relacionada con VIE ERP (FK contract.CUPSEntityContractDescriptions). INT, vinculación a catálogo de servicios CUPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio RIPS (Registro Individual de Prestación de Servicios). CHAR(20), identificador del procedimiento/servicio en facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: activo/inactivo. BIT (1=habilitado, 0=deshabilitado), control de vigencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'estado del registo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rango máximo de litros por minuto para administración del gas. DECIMAL(18,2), límite superior de flujo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'RANGMAXIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'rango maximo de litros por minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'RANGMAXIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'RANGMAXIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rango mínimo de litros por minuto para administración del gas. DECIMAL(18,2), límite inferior de flujo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'RANGMINIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'rango minimo de litros por minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'RANGMINIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'RANGMINIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la vía de administración (inhalatoria, intravenosa, etc.). CHAR(50), especificación del método de suministro terapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'DESVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la via de administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'DESVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'DESVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la vía de administración del medicamento/gas. CHAR(3), PK, clasificador de ruta terapéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Vida de administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración de vías de administración de medicamentos o servicios clínicos, incluyendo los rangos de dosis permitidos y su relación con el servicio (CUPS/IPS) y tipo de gas medicinal. Permite controlar y validar las vías de administración habilitadas en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO';
