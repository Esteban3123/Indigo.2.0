CREATE TABLE [Contract].[IPSServiceAlternos] (
    [Code]          VARCHAR (20) NOT NULL,
    [CIRUJANO]      VARCHAR (20) NOT NULL,
    [ANESTESIOLOGO] VARCHAR (20) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del anestesiólogo alterno (profesional de la salud especializado en anestesia); tipo VARCHAR(20), usado en configuración de servicios quirúrgicos alternativos del contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSServiceAlternos', @level2type = N'COLUMN', @level2name = N'ANESTESIOLOGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código del Anestesiologo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSServiceAlternos', @level2type = N'COLUMN', @level2name = N'ANESTESIOLOGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSServiceAlternos', @level2type = N'COLUMN', @level2name = N'ANESTESIOLOGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del cirujano alterno (profesional de la salud especializado en cirugía); tipo VARCHAR(20), usado en configuración de servicios quirúrgicos alternativos del contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSServiceAlternos', @level2type = N'COLUMN', @level2name = N'CIRUJANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código del Cirujano.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSServiceAlternos', @level2type = N'COLUMN', @level2name = N'CIRUJANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSServiceAlternos', @level2type = N'COLUMN', @level2name = N'CIRUJANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador del registro de servicios alternativos (cirujanos y anestesiólogos suplentes/respaldos); tipo VARCHAR(20), clave primaria de la tabla de configuración contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSServiceAlternos', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código identificador de los alternos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSServiceAlternos', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSServiceAlternos', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicios alternos de contratos IPS que asocian un procedimiento o servicio con el cirujano y el anestesiólogo asignados. Permite gestionar la cobertura contractual de intervenciones quirúrgicas indicando qué profesionales participan.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSServiceAlternos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSServiceAlternos';
