CREATE TABLE [dbo].[HCRIESGOSP] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRCES]    CHAR (15)                                                                        NOT NULL,
    [GESTACION]     TINYINT                                                                          NULL,
    [SIFGESCON]     TINYINT                                                                          NULL,
    [HIPERINDGES]   TINYINT                                                                          NULL,
    [HIPTERCON]     TINYINT                                                                          NULL,
    [SINTOMRESP]    TINYINT                                                                          NULL,
    [TUBERMULT]     TINYINT                                                                          NULL,
    [LEPRA]         TINYINT                                                                          NULL,
    [OBESDESNPRO]   TINYINT                                                                          NULL,
    [VICTMALTRATO]  TINYINT                                                                          NULL,
    [VICTVIOLSEX]   TINYINT                                                                          NULL,
    [INFTRASEX]     TINYINT                                                                          NULL,
    [ENFMENTAL]     TINYINT                                                                          NULL,
    [CANCERVIX]     TINYINT                                                                          NULL,
    [CANSENO]       TINYINT                                                                          NULL,
    [FECGESTA]      DATETIME                                                                         NULL,
    [FECSIFGES]     DATETIME                                                                         NULL,
    [FECHIPER]      DATETIME                                                                         NULL,
    [FECHIPOTE]     DATETIME                                                                         NULL,
    [FECSINTO]      DATETIME                                                                         NULL,
    [FECTUBER]      DATETIME                                                                         NULL,
    [FECLEPRA]      DATETIME                                                                         NULL,
    [FECOBEDES]     DATETIME                                                                         NULL,
    [FECVICMAL]     DATETIME                                                                         NULL,
    [FECVICVIOSEX]  DATETIME                                                                         NULL,
    [FECINFTRA]     DATETIME                                                                         NULL,
    [FECENFMEN]     DATETIME                                                                         NULL,
    [FECCANCER]     DATETIME                                                                         NULL,
    [FECCANSEN]     DATETIME                                                                         NULL,
    [LEISHMANIO]    TINYINT                                                                          NULL,
    [FECLEISHMA]    DATETIME                                                                         NULL,
    [VICTVIOLESEXU] INT                                                                              NULL,
    [ViolenceType]  TINYINT                                                                          NULL,
    [BurnedPerson]  BIT                                                                              NULL,
    [VICTCONFLI]    BIT                                                                              NULL,
    CONSTRAINT [PK_HCRIESGOSP] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRIESGOSP_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCRIESGOSP].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_HCRIESGOSP__NUMINGRCES__INC__GESTACION]
    ON [dbo].[HCRIESGOSP]([NUMINGRCES] ASC)
    INCLUDE([GESTACION]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_HCRIESGOSP__IPCODPACI__NUMINGRCES]
    ON [dbo].[HCRIESGOSP]([IPCODPACI] ASC, [NUMINGRCES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Víctima de conflicto armado (BIT): Sí/No. Indicador de exposición a violencia por conflicto, usado en evaluación de riesgo poblacional y RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTCONFLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Victima de conflicto: True -> Si - False -> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTCONFLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTCONFLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Paciente con quemaduras (BIT): Sí/No. Indica lesiones por quemadura, relevante en evaluación de trauma y urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'BurnedPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el paciente presenta quemaduras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'BurnedPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'BurnedPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de violencia (TINYINT): 1=Física, 2=Psicológica, 3=Negligencia/abandono, 4=Sexual. Clasificación de violencia; obligatorio si VICTVIOLESEXU=2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'ViolenceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de violencia:  1 - Física  2 - Psicológica  3 - Neglicencia y abandono  4 - Sexual    (Debe estar llena cuando el campo VICTVIOLESEXU es 2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'ViolenceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'ViolenceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Víctima de violencia sexual (INT): 1=Sí, 2=No. Indicador de abuso sexual en paciente, crítico para protección y referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTVIOLESEXU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Victima de violencia sexual: 1-> Si 2-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTVIOLESEXU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTVIOLESEXU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro de leishmaniosis (DATETIME): Momento de identificación de parásito Leishmania, leishmaniasis cutánea o visceral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECLEISHMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro de Leishmaniosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECLEISHMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECLEISHMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Leishmaniosis (TINYINT): 1=Sí. Infección parasitaria tropical, endémica en Colombia, búsqueda: leishmaniasis, enfermedad de Chagas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'LEISHMANIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Leishmaniosis 1:si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'LEISHMANIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'LEISHMANIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de cáncer de seno (DATETIME): Registro de detección de neoplasia mamaria, carcinoma de mama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECCANSEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro de cancer de seno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECCANSEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECCANSEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de cáncer cervicouterino (DATETIME): Registro de cáncer de cuello uterino, carcinoma de cérvix.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECCANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro de cancer cervix', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECCANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECCANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de enfermedad mental (DATETIME): Registro de trastorno psiquiátrico (ansiedad, depresión, esquizofrenia, TDAH, consumo, bipolar).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECENFMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro de enfermedades mentales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECENFMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECENFMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de ITS (DATETIME): Registro de infección de transmisión sexual (gonorrea, clamidia, sífilis no gestacional, VIH).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECINFTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro de infecciones de transmisión sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECINFTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECINFTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro de violencia sexual (DATETIME): Documentación de abuso, asalto o explotación sexual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECVICVIOSEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro de violencia sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECVICVIOSEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECVICVIOSEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro de maltrato (DATETIME): Documentación de abuso físico, emocional o negligencia (mujer, menor de edad).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECVICMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro de victima de maltrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECVICMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECVICMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de obesidad o desnutrición (DATETIME): Registro de IMC elevado o desnutrición proteico-calórica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECOBEDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro de Obecidad o desnutricion proteico calorica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECOBEDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECOBEDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de lepra (DATETIME): Registro de enfermedad de Hansen (paucibacilar o multibacilar).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECLEPRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro de lepra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECLEPRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECLEPRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de tuberculosis (DATETIME): Registro de TB pulmonar o extrapulmonar, baciloscopia positiva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECTUBER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro de tubercuosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECTUBER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECTUBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro de sintomático respiratorio (DATETIME): Documentación de tos ≥2 semanas, sospecha TB pulmonar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECSINTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro de sintomatico respiratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECSINTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECSINTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de hipotiroidismo congénito (DATETIME): Registro de déficit hormonal tiroideo neonatal, screening neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECHIPOTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro de Hipoteroidismo congenito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECHIPOTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECHIPOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de preeclampsia/eclampsia (DATETIME): Hipertensión inducida por gestación, riesgo materno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECHIPER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro de Hipertencion inducida por la gestacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECHIPER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECHIPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de sífilis gestacional (DATETIME): Registro de sífilis materna o sífilis congénita neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECSIFGES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro de Sifilis Gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECSIFGES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECSIFGES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro de gestación (DATETIME): Inicio de embarazo o semana gestacional documentada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECGESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro de Gestación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECGESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECGESTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cáncer de mama (TINYINT): 1=Sí, 2=No, 21=Riesgo no evaluado. Carcinoma mamario, neoplasia maligna de glándula mamaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'CANSENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cancer de seno:  1-> si  2-> no  21->riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'CANSENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'CANSENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cáncer de cérvix (TINYINT): 0=N/A, 1=Sí, 2=No, 21=Riesgo no evaluado. Carcinoma cervicouterino, cáncer de cuello uterino.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'CANCERVIX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cancer de cérvix  0-> no aplica  1-> si  2-> no  21->riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'CANCERVIX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'CANCERVIX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Enfermedad mental (TINYINT): 1=Ansiedad, 2=Depresión, 3=Esquizofrenia, 4=TDAH, 5=Consumo SPA, 6=Bipolar, 7=No, 21=No evaluado. Diagnóstico psiquiátrico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'ENFMENTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Enfermedad Mental  1-> si es diagnostico de ansiedad  2-> si es diagnostico de depresion  3-> si es diagnostico de esquizofrenia  4-> si es diagnostico de deficit de atencion por hiperactividad  5-> si es diagnostico de consumo de sustancias psicoactivas  6-> si es diagnostico de trastorno del ánimo bipolar  7-> no  21-> riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'ENFMENTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'ENFMENTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Infección de transmisión sexual (TINYINT): 1=Sí, 2=No, 21=No evaluado. ITS, ETS, gonorrea, clamidia, sífilis, VIH.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'INFTRASEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Infeccion de transmisión sexual  1-> si  2-> no  21->riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'INFTRASEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'INFTRASEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Víctima de violencia sexual (TINYINT): 1=Sí, 2=No, 21=No evaluado. Abuso sexual, asalto sexual, explotación sexual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTVIOLSEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Victima de violencia sexual  1-> si  2-> no  21->riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTVIOLSEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTVIOLSEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Víctima de maltrato (TINYINT): 0=N/A, 1=Mujer maltratada, 2=Menor maltratado, 3=No, 21=No evaluado. Violencia doméstica, abuso infantil.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTMALTRATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Victima de maltrato  0-> no aplica   1-> si es mujer victima de maltrato  2-> si es menor victima de maltrato  3-> no  21-> Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTMALTRATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTMALTRATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obesidad o desnutrición (TINYINT): 1=Obesidad, 2=Desnutrición proteico-calórica, 3=No, 21=No evaluado. IMC, estado nutricional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'OBESDESNPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obesidad o desnutricion proteicocalorica  1-> si es obesidad  2-> si es desnutricion proteico calórica  3-> no  21-> riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'OBESDESNPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'OBESDESNPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lepra/Enfermedad de Hansen (TINYINT): 1=Paucibacilar, 2=Multibacilar, 3=No, 21=No evaluado. Infección Mycobacterium leprae.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'LEPRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lepra  1-> lepra paucibacilar  2-> lepra multibacilar  3-> no  21-> riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'LEPRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'LEPRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tuberculosis multidrogoresistente (TINYINT): 0=N/A, 1=Sí, 2=No, 21=No evaluado. TB-MDR, TB resistente a fármacos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'TUBERMULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tuberculosis Multidrogoresistente  0-> no aplica  1-> si  2-> no  21->riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'TUBERMULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'TUBERMULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sintomático respiratorio (TINYINT): 1=Sí, 2=No, 21=No evaluado. Tos persistente, sospecha TB, síntomas pulmonares.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'SINTOMRESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sintomático Respitatorio  1-> si  2-> no  21->riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'SINTOMRESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'SINTOMRESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hipotiroidismo congénito (TINYINT): 0=N/A, 1=Sí, 2=No, 21=No evaluado. Déficit hormonal tiroideo neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'HIPTERCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hipoteroidismo congénito  0-> no aplica  1-> si  2-> no  21->riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'HIPTERCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'HIPTERCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Preeclampsia/eclampsia gestacional (TINYINT): 0=N/A, 1=Sí, 2=No, 21=No evaluado. Hipertensión inducida por embarazo (requiere GESTACION=1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'HIPERINDGES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hipertencion inducida por la gestación  0-> no aplica  1-> si  2-> no  21->riesgo no evaluado    si este campo es = 1, el campo gestación debe ser tambien = 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'HIPERINDGES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'HIPERINDGES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sífilis gestacional/congénita (TINYINT): 0=N/A, 1=Sífilis materna gestacional, 2=Sífilis congénita neonatal, 3=No, 21=No evaluado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'SIFGESCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sifilis Gestacional:  0-> no Aplica  1->si es mujer con sifilis gestacional  2-> si es recien nacido con sifilis congenita  3-> no  21-> riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'SIFGESCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'SIFGESCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de gestación (TINYINT): 0=N/A, 1=Embarazada/gestante, 2=No gestante, 21=No evaluado. Embarazo, estado de gravidez.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'GESTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la paciente esta en Gestación:   0-> no aplica  1-> si  2-> no  21-> Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'GESTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'GESTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número/código del ingreso (CHAR 15): Identificador único de la atención, episodio hospitalario, consulta o urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'NUMINGRCES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'NUMINGRCES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'NUMINGRCES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, MASKED): Identificación PII del paciente, cédula, número de documento, equivalente a identificación único.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY): Clave primaria tabla HCRIESGOSP, secuencia automática 1:1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumérico tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de factores de riesgo en salud pública detectados durante el ingreso del paciente. Almacena indicadores clínicos y sociales como gestación de alto riesgo, enfermedades de notificación obligatoria (tuberculosis, lepra, leishmaniasis), infecciones de transmisión sexual, salud mental, cáncer, obesidad, y situaciones de vulnerabilidad como víctimas de maltrato, violencia sexual o conflicto armado, junto con las fechas en que cada riesgo fue identificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP';
