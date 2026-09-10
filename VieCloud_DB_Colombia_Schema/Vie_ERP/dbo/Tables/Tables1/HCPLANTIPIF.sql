CREATE TABLE [dbo].[HCPLANTIPIF] (
    [Id]        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODDIAGNO] VARCHAR (6)  NOT NULL,
    [CODTIPIFI] VARCHAR (50) NULL,
    CONSTRAINT [PK_HCPLANTIPIF] PRIMARY KEY CLUSTERED ([Id] ASC, [CODDIAGNO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipificación de diagnóstico/condición de salud pública (1-39): gestación, sífilis congénita, hipertensión gestacional, hipotiroidismo congénito, TB multidrogoresistente, lepra, obesidad, desnutrición, maltrato, violencia sexual, ITS, enfermedades mentales (ansiedad, depresión, esquizofrenia, TDAH, psicosustancias, trastorno bipolar), cánceres (cérvix, seno), parto/cesárea, control RN, planificación familiar, control prenatal, oftalmología, crecimiento/desarrollo, hepatitis B, VIH, leishmaniasis, hipertensión arterial, diabetes mellitus, enfermedad renal crónica, cáncer hematolinfático. VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANTIPIF', @level2type = N'COLUMN', @level2name = N'CODTIPIFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el codigo de la tipificación  1. Gestación  2. Sífilis Gestacional o Congénita   4. Hipertensión inducida por la gestación  5. Hipotiroidismo congénito  6. Sintomático respiratorio  7. Tuberculosis multidrogoresistente  8. Lepra Paucibacilar o Multibacilar  10. Obesidad  11. Desnutrición proteico Calórica  12. Víctima de Maltrato   13. Víctima de Violencia Sexual  14. Infecciones de Trasmisión Sexual  15. Enfermedad Mental – Ansiedad  16. Enfermedad Mental - Depresión  17. Enfermedad Mental - Esquizofrenia  18. Enfermedad Mental – Déficit de atención por Hiperactividad  19. Enfermedad Mental – Consumo de sustancias Psicoactivas  20. Enfermedad Mental – Trastorno del Animo Bipolar  21. Cáncer de Cérvix  22. Cáncer de Seno  23. Atención del parto o cesárea  24. Control Recién Nacido   25. Planificación Familiar Primera vez   26. Control Prenatal de Primera vez   27. Control Prenatal   28. Último Control Prenatal   29. Valoración de la Agudeza Visual   30. Consulta por Oftalmología   31. Consulta de Crecimiento y Desarrollo Primera vez   32. Antígeno de Superficie Hepatitis B en Gestantes   33. VIH   34. Baciloscopia de Diagnóstico   35.              Leishmaniasis  36.              Hipertensión Arterial Sistémica (RES-2463)  37.              Diabetes Mellitus Tipo I o Tipo II (RES-2463)  38.              Enfermedad Renal Crónica en cualquier estadio (RES-2463)  39               Cancer Hematolinfático (RES- 247)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANTIPIF', @level2type = N'COLUMN', @level2name = N'CODTIPIFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANTIPIF', @level2type = N'COLUMN', @level2name = N'CODTIPIFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 o clasificación diagnóstica asociada a la tipificación de salud pública. Clave foránea (FK) para vincular diagnósticos de Historia Clínica. VARCHAR(6).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANTIPIF', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnostico asociado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANTIPIF', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANTIPIF', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (autonumérico/identity) de la relación entre diagnóstico y tipificación. INT IDENTITY(1,1), parte de clave primaria compuesta. No replicable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANTIPIF', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumérico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANTIPIF', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANTIPIF', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona diagnósticos (CIE-10) con tipos de planilla o formulario de historia clínica, definiendo qué tipo de plantilla de ingreso de información clínica corresponde a cada diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANTIPIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANTIPIF';
