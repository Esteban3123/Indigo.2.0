CREATE TABLE [dbo].[PRHCPAGE] (
    [ID]         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC] INT NOT NULL,
    [CODPAGE]    INT NOT NULL,
    [ORDEN]      INT NOT NULL,
    CONSTRAINT [PK_PRHCPAGE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCPAGE_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCPAGE] NOCHECK CONSTRAINT [FK_PRHCPAGE_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de orden/posición que ocupa esta sección PAGE dentro del modelo de HC. Define el flujo visual y lógico de captura de datos en la historia clínica electrónica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'ORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden que lleva el page dentro del modelo de HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'ORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'ORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código numérico del PAGE/sección estándar de la HC: 1=MC/EA, 2=Antecedentes, 3=Revisión por sistemas, 4=Examen físico, 5=Valoración paraclínicos, 6=Análisis, 7=Impresión diagnóstica, 8=Plan de manejo, 9=Destino del paciente, 10=Odontograma, 11=Periodontograma, 12=AIEPI, 13=Información médica general, 14=Escalas, 15=Valoración paraclínicos histórico, 16=Otros, 17=Valoración clínica optometría, 18=Valoración materno perinatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'CODPAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del PAGE de la HC - CONTROLES ESTANDAR   
1. MC / EA  
2. ANTECEDENTES  
3. REVISION POR SISTEMAS
4. EXAMEN FISICO  
5. VALORACION DE PARACLINICOS
6. ANALISIS  
7. IMPRESION DIAGNOSTICA
8. PLAN DE MANEJO  
9. DESTINO DEL PACIENTE 
10. ODONTOGRAMA 
11.PERIODONTOGRAMA
12.AIEPI  
13.INFORMACION MEDICA GENERAL
14.ESCALAS  
15.VALORACION PARACLINICOS HISTORICO 
16.OTROS  
17.VALORACION CLINICA DE OPTOMETRIA
18.Valoración materno perinatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'CODPAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'CODPAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del modelo de historia clínica (HC) al que pertenece la página. Referencia a PRMODELOHC.ID. Vincula página a plantilla/estructura de HC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con el modelo de HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la página en el modelo de historia clínica. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Páginas o secciones que componen cada modelo de historia clínica. Define la estructura y el orden de presentación de las páginas dentro de un formulario o plantilla de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE';
