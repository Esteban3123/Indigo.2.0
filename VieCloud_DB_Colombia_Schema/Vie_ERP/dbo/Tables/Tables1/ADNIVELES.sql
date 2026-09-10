CREATE TABLE [dbo].[ADNIVELES] (
    [NIVCODIGO] CHAR (2)       NOT NULL,
    [NIVDESCRI] CHAR (100)     NOT NULL,
    [NIVPORCMO] NUMERIC (6, 2) NOT NULL,
    [NIVPORCOP] NUMERIC (6, 2) NOT NULL,
    [NIVPORSUB] NUMERIC (6, 2) NOT NULL,
    [NIVPORVIN] NUMERIC (6, 2) NOT NULL,
    [NIVSISBEN] INT            NOT NULL,
    [TOPEVECMO] MONEY          NOT NULL,
    [TOPEVECOP] MONEY          NOT NULL,
    [TOPEVESUB] MONEY          NOT NULL,
    [TOPEVEVIN] MONEY          NOT NULL,
    [TOPANUCMO] MONEY          NOT NULL,
    [TOPANUCOP] MONEY          NOT NULL,
    [TOPANUSUB] MONEY          NOT NULL,
    [TOPANUVIN] MONEY          NOT NULL,
    [INDAUDFOR] NUMERIC (18)   NOT NULL,
    CONSTRAINT [PK_ADNiveles] PRIMARY KEY CLUSTERED ([NIVCODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría (NUMERIC 18); identificador único para trazabilidad y control de cambios en la configuración de niveles.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tope anual vinculados (MONEY); límite máximo de cobertura anual en pesos para pacientes vinculados sin afiliación formal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUVIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tope anual vinculados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUVIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUVIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tope anual régimen subsidiado (MONEY); límite máximo de cobertura anual en pesos para población afiliada al régimen subsidiado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUSUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tope anual regimen subsidiado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUSUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUSUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tope anual régimen contributivo (MONEY); límite máximo de cobertura anual en pesos para población afiliada al régimen contributivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUCOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tope anual regimen contributivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUCOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUCOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tope anual cuota moderadora (MONEY); límite máximo anual en pesos que el paciente aporta como cuota moderadora por servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUCMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tope anual cuota moderadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUCMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPANUCMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tope evento vinculados (MONEY); límite máximo en pesos por evento/atención para pacientes vinculados sin afiliación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVEVIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tope evento vinculados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVEVIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVEVIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tope evento régimen subsidiado (MONEY); límite máximo en pesos por evento/atención para población en régimen subsidiado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVESUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tope vento regimen subsidiado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVESUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVESUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tope evento régimen contributivo (MONEY); límite máximo en pesos por evento/atención para población en régimen contributivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVECOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tope evento regimen contributivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVECOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVECOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tope evento cuota moderadora (MONEY); límite máximo en pesos por evento que el paciente paga como cuota moderadora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVECMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tope  evento cuota moderadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVECMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'TOPEVECMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel SISBEN (INT); clasificación socioeconómica del paciente según Sistema de Identificación de Potenciales Beneficiarios (1-4).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVSISBEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel del Sisben', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVSISBEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVSISBEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje copago vinculados (NUMERIC 6,2); porcentaje de participación del paciente vinculado en el costo del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORVIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del copago para pacientes vinculados del nivel o estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORVIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORVIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje copago régimen subsidiado (NUMERIC 6,2); porcentaje de participación del afiliado subsidiado en el costo del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORSUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del copago para el regimen subsidiado para el nivel o estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORSUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORSUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje copago régimen contributivo (NUMERIC 6,2); porcentaje de participación del afiliado contributivo en el costo del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORCOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del copago para el regimen contributivo del nivel o estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORCOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORCOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje cuota moderadora (NUMERIC 6,2); porcentaje de participación del paciente por servicios prestados como cuota moderadora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORCMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de la cuota moderadora para el nivel o estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORCMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVPORCMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del nivel o estrato (CHAR 100); nombre/clasificación socioeconómica del nivel (SISBEN 1-4, estrato 1-6, o equivalente).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Nivel o Estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del nivel o estrato (CHAR 2, PK); identificador único del nivel o estrato socioeconómico configurado en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Nivel o Estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES', @level2type = N'COLUMN', @level2name = N'NIVCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Niveles de atención en salud con sus porcentajes de copago y topes de gasto por modalidad de afiliación (contributivo, subsidiado, vinculado). Define cuánto paga el paciente según su nivel SISBEN y el techo máximo de cobro por evento y por año.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELES';
