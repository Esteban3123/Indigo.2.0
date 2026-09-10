CREATE TABLE [dbo].[HCFICHA880] (
    [ID]                  INT            IDENTITY (1, 1) NOT NULL,
    [IDFICHANOTIFICACION] INT            NOT NULL,
    [ERUPCION]            INT            NULL,
    [TIPOERUPCION]        INT            NULL,
    [FECHAINIERUPCION]    DATE           NULL,
    [FIEBRE]              INT            NULL,
    [ULCEGENITALES]       INT            NULL,
    [OTROSINTOMAS]        INT            NULL,
    [CUALESOTROSINTOMAS]  VARCHAR (1000) NULL,
    [COMPLICACIONES]      INT            NULL,
    [TUVOCONTPROBA]       INT            NULL,
    [FECHACONTACTO]       DATE           NULL,
    [TUVOCONTSEXUAL]      INT            NULL,
    [TIENENUEVAPSEXUAL]   INT            NULL,
    [ANTEVIAJEEXT21D]     INT            NULL,
    [CUALPAISEXTVIAJO]    VARCHAR (100)  NULL,
    [SEGUICONTACTOS]      INT            NULL,
    [FECHAFINALSEGCONT]   DATE           NULL,
    [FUENTEINFECCION]     INT            NULL,
    [CASODESCARTADO]      INT            NULL,
    [COMPLICEREBRAL]      BIT            NULL,
    [COMPLIPULMONAR]      BIT            NULL,
    [COMPLIOFTALMICA]     BIT            NULL,
    [FECHATOMA1]          DATE           NULL,
    [FECHARECEP1]         DATE           NULL,
    [MUESTRA1]            INT            NULL,
    [PRUEBA1]             INT            NULL,
    [AGENTE1]             INT            NULL,
    [RESULTADO1]          INT            NULL,
    [FECHARES1]           DATE           NULL,
    [VALOREGIS1]          VARCHAR (20)   NULL,
    [FECHATOMA2]          DATE           NULL,
    [FECHARECEP2]         DATE           NULL,
    [MUESTRA2]            INT            NULL,
    [PRUEBA2]             INT            NULL,
    [AGENTE2]             INT            NULL,
    [RESULTADO2]          INT            NULL,
    [FECHARES2]           DATE           NULL,
    [VALOREGIS2]          VARCHAR (20)   NULL,
    [FECHATOMA3]          DATE           NULL,
    [FECHARECEP3]         DATE           NULL,
    [MUESTRA3]            INT            NULL,
    [PRUEBA3]             INT            NULL,
    [AGENTE3]             INT            NULL,
    [RESULTADO3]          INT            NULL,
    [FECHARES3]           DATE           NULL,
    [VALOREGIS3]          VARCHAR (20)   NULL,
    [FECHATOMA4]          DATE           NULL,
    [FECHARECEP4]         DATE           NULL,
    [MUESTRA4]            INT            NULL,
    [PRUEBA4]             INT            NULL,
    [AGENTE4]             INT            NULL,
    [RESULTADO4]          INT            NULL,
    [FECHARES4]           DATE           NULL,
    [VALOREGIS4]          VARCHAR (20)   NULL,
    [VERSION]             VARCHAR (20)   NULL,
    [JSON]                VARCHAR (MAX)  NULL,
    [CODDIAGNO]           NCHAR (4)      NULL,
    CONSTRAINT [PK_HCFICHA880] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA880_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA880_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA880] NOCHECK CONSTRAINT [CK_HCFICHA880_JSON];




GO
ALTER TABLE [dbo].[HCFICHA880] NOCHECK CONSTRAINT [CK_HCFICHA880_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 (4 caracteres), clasificación clínica del caso notificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Objeto JSON con metadatos internacionales: PAIS_TEXTO (nombre país viaje), PAIS_CODIGO (código ISO 3 dígitos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PAIS_TEXTO = texto (Nombre Pais internacional viaje)    PAIS_CODIGO = texto (Codigo Pais internacional viaje, 3 digitos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del formulario de notificación epidemiológica; NULL indica primera versión/captura inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor registrado manualmente en la cuarta muestra cuando resultado=6', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resultado del cuarto ensayo de laboratorio (PCR, serología)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del resultado 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuarto resultado laboratorial: 1=Positivo, 2=Negativo, 3=No procesado, 4=Inadecuado, 5=Dudoso, 6=Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado:   1= Positivo   2= Negativo   3= No procesado   4= Inadecuado   5= Dudoso  6= Valor registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuarto agente identificado: 1=Virus Monkeypox (mpox), 2=Otro virus o patógeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente:   1=J Monkeypox   2= Otro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuarto tipo de prueba diagnóstica: 1=PCR (reacción cadena polimerasa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba:  1=PCR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuarto tipo de espécimen: 1=Hisopado nasofaríngeo, 2=Tejido, 3=LCR, 4=Suero, 5=Hisopado orofaríngeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestra: 1= Hisopado nasofaríngeo   2= Tejido  3= Lcr   4= Suero   5=Orofaríngeo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción en laboratorio de la cuarta muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de la cuarta muestra clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor registrado manualmente en la tercera muestra cuando resultado=6', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resultado del tercer ensayo de laboratorio (PCR, serología)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del resultado 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercer resultado laboratorial: 1=Positivo, 2=Negativo, 3=No procesado, 4=Inadecuado, 5=Dudoso, 6=Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado:   1= Positivo   2= Negativo   3= No procesado   4= Inadecuado   5= Dudoso  6= Valor registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercer agente identificado: 1=Virus Monkeypox (mpox), 2=Otro virus o patógeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente:   1=J Monkeypox   2= Otro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercer tipo de prueba diagnóstica: 1=PCR (reacción cadena polimerasa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba:   1=PCR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercer tipo de espécimen: 1=Hisopado nasofaríngeo, 2=Tejido, 3=LCR, 4=Suero, 5=Hisopado orofaríngeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestra:   1= Hisopado nasofaríngeo   2= Tejido  3= Lcr   4= Suero  5=Orofaríngeo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción en laboratorio de la tercera muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de la tercera muestra clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor registrado manualmente en la segunda muestra cuando resultado=6', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resultado del segundo ensayo de laboratorio (PCR, serología)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del resultado 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo resultado laboratorial: 1=Positivo, 2=Negativo, 3=No procesado, 4=Inadecuado, 5=Dudoso, 6=Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado:   1= Positivo   2= Negativo   3= No procesado   4= Inadecuado   5= Dudoso  6= Valor registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo agente identificado: 1=Virus Monkeypox (mpox), 2=Otro virus o patógeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente:   1=J Monkeypox   2= Otro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo tipo de prueba diagnóstica: 1=PCR (reacción cadena polimerasa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba:   1=PCR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo tipo de espécimen: 1=Hisopado nasofaríngeo, 2=Tejido, 3=LCR, 4=Suero, 5=Hisopado orofaríngeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestra:   1= Hisopado nasofaríngeo   2= Tejido  3= Lcr   4= Suero  5=Orofaríngeo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción en laboratorio de la segunda muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de la segunda muestra clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor registrado manualmente en la primera muestra cuando resultado=6', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resultado del primer ensayo de laboratorio (PCR, serología)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del resultado 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer resultado laboratorial: 1=Positivo, 2=Negativo, 3=No procesado, 4=Inadecuado, 5=Dudoso, 6=Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado:   1= Positivo   2= Negativo   3= No procesado   4= Inadecuado   5= Dudoso  6= Valor registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer agente identificado: 1=Virus Monkeypox (mpox), 2=Otro virus o patógeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente:   1=J Monkeypox   2= Otro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer tipo de prueba diagnóstica: 1=PCR (reacción cadena polimerasa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba:   1=PCR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer tipo de espécimen: 1=Hisopado nasofaríngeo, 2=Tejido, 3=LCR, 4=Suero, 5=Hisopado orofaríngeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestra:   1= Hisopado nasofaríngeo   2= Tejido  3= Lcr   4= Suero  5=Orofaríngeo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción en laboratorio de la primera muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de la primera muestra clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación oftálmica presente (True=Sí, False=No); secuela ocular de mpox', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLIOFTALMICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'COMPLIOFTALMICA:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLIOFTALMICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLIOFTALMICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación pulmonar presente (True=Sí, False=No); afectación respiratoria de mpox', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLIPULMONAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'COMPLIPULMONAR:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLIPULMONAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLIPULMONAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación neurológica/cerebral presente (True=Sí, False=No); afectación SNC de mpox', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLICEREBRAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'COMPLICEREBRAL:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLICEREBRAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLICEREBRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio de descarte: 1=Lab negativo, 2=Dengue, 3=Herpes 6/6, 4=Reacción alérgica, 5=Varicela, 6=Sífilis, 7=Otro diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CASODESCARTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si el caso es descartado, señale el criterio para descartar:   1=Laboratorio negativo  2=Dengue  3=Herpes 6,6  4=Reacción alérgica  5=Varicela  6=Sífilis  7=Otro diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CASODESCARTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CASODESCARTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen probable del contagio (casos confirmados): 1=Importado, 2=Relacionado importación, 3=Fuente desconocida, 4=Relacionado desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FUENTEINFECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si el caso fue confirmado, señale fuente de infección:   1=Importado  2=Relacionado con la importación  3=Fuente desconocida  4=Relacionado con desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FUENTEINFECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FUENTEINFECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final del período de seguimiento de contactos expuestos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHAFINALSEGCONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final de seguimiento a contactos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHAFINALSEGCONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHAFINALSEGCONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Realización de seguimiento epidemiológico a contactos: 1=Sí, 2=No, 3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'SEGUICONTACTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' ¿Se realizó seguimiento a contactos? :  1=Si  2=No  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'SEGUICONTACTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'SEGUICONTACTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País o región internacional donde viajó el paciente (últimos 21 días)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CUALPAISEXTVIAJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Donde: (lugar internacional)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CUALPAISEXTVIAJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CUALPAISEXTVIAJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de viaje internacional en últimos 21 días: 1=Sí, 2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ANTEVIAJEEXT21D';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Antecedentes de viaje al exterior en los últimos 21 días :  1=Si  2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ANTEVIAJEEXT21D';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ANTEVIAJEEXT21D';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de nueva pareja o múltiples parejas sexuales: 1=Sí, 2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TIENENUEVAPSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' ¿Tiene nueva o múltiples parejas sexuales? :  1=Si  2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TIENENUEVAPSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TIENENUEVAPSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contacto estrecho o sexual con persona procedente del extranjero: 1=Sí, 2=No, 3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TUVOCONTSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' ¿Tuvo contacto estrecho (incluso sexual) con personal procedente del extranjero? :  1=Si  2=No  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TUVOCONTSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TUVOCONTSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del contacto estrecho o de exposición inicial documentada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHACONTACTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHACONTACTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHACONTACTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Exposición a caso probable/confirmado de Monkeypox últimos 21 días: 1=Sí, 2=No, 3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TUVOCONTPROBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Tuvo contacto con caso probable/Confirmado de Monkeypox en los últimos 21 días? :  1=Si  2=No  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TUVOCONTPROBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TUVOCONTPROBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de complicaciones clínicas durante enfermedad: 1=Sí, 2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLICACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones:  1=Si   2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLICACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLICACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de otros signos/síntomas adicionales no listados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CUALESOTROSINTOMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CUALES OTROS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CUALESOTROSINTOMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CUALESOTROSINTOMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de signos/síntomas adicionales no mencionados: 1=Sí, 2=No, 3=Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'OTROSINTOMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros signos/síntomas:  1=Si   2=No  3=Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'OTROSINTOMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'OTROSINTOMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Úlceras o lesiones en área genital/anal: 1=Sí, 2=No, 3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ULCEGENITALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Úlceras genitales:   1=Si  2=No  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ULCEGENITALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ULCEGENITALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de fiebre: 1=Sí, 2=No, 3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fiebre:   1=Si   2=No  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de la erupción cutánea (exantema primario)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHAINIERUPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inicio de erupción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHAINIERUPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHAINIERUPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Morfología del exantema: 1=Vesicular, 2=Máculopapular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TIPOERUPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Erupcion:   1=Vesicular   2=Máculopapular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TIPOERUPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TIPOERUPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de erupción cutánea/exantema: 1=Sí, 2=No, 3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ERUPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ERUPCION:   1=Si   2=No  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ERUPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ERUPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a tabla HCFICHANOTIFICACION; identificador único del caso notificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  el Id de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autoincremental; identificador único del registro de ficha epidemiológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha epidemiológica 880 para notificación de casos de viruela símica (monkeypox) y enfermedades exantemáticas similares. Registra síntomas, contactos de riesgo, antecedentes de viaje, seguimiento de contactos y resultados de laboratorio de hasta cuatro muestras por caso notificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880';
