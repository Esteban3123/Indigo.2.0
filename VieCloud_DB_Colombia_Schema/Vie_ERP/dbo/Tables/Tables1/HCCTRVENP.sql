CREATE TABLE [dbo].[HCCTRVENP] (
    [CONSECUTI]             NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]             VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]             CHAR (10)                                                                        NOT NULL,
    [CODCENATE]             CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]             CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]             CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [ESCATHEPA]             BIT                                                                              NOT NULL,
    [NOMCATETE]             CHAR (60)                                                                        NULL,
    [NUMCATETE]             CHAR (10)                                                                        NULL,
    [NOMVENUTI]             CHAR (60)                                                                        NOT NULL,
    [EXTREMIDA]             CHAR (2)                                                                         NULL,
    [ESTACTIVA]             BIT                                                                              NOT NULL,
    [FECHAINIC]             DATETIME                                                                         NOT NULL,
    [FECHAFINA]             DATETIME                                                                         NULL,
    [MOTITERMI]             VARCHAR (300)                                                                    NULL,
    [CODPROACT]             CHAR (20)                                                                        NULL,
    [FECREGINI]             DATETIME                                                                         NOT NULL,
    [FECREGFIN]             DATETIME                                                                         NULL,
    [CatheterTypeId]        INT                                                                              NULL,
    [NumberOfVenipuncture]  INT                                                                              NULL,
    [DiscontinuationReason] INT                                                                              NULL,
    [CODCENATESUS]          CHAR (10)                                                                        NULL,
    [UFUCODIGOSUS]          CHAR (10)                                                                        NULL,
    CONSTRAINT [PK_HCCTRVENP] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCCTRVENP_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRVENP_ADCENATEN1] FOREIGN KEY ([CODCENATESUS]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRVENP_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCTRVENP_CatheterType] FOREIGN KEY ([CatheterTypeId]) REFERENCES [dbo].[CatheterType] ([Id]),
    CONSTRAINT [FK_HCCTRVENP_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCTRVENP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRVENP_INPROFSAL1] FOREIGN KEY ([CODPROACT]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRVENP_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCCTRVENP_INUNIFUNC1] FOREIGN KEY ([UFUCODIGOSUS]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRVENP].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRVENP].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
CREATE NONCLUSTERED INDEX [IX_HCCTRVENP__IPCODPACI__NUMINGRES__NOMVENUTI]
    ON [dbo].[HCCTRVENP]([IPCODPACI] ASC, [NUMINGRES] ASC, [NOMVENUTI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRVENB]
    ON [dbo].[HCCTRVENP]([NUMINGRES] ASC, [IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (centro, servicio, departamento) desde donde se discontinúa o suspende el acceso vascular periférico; FK a INUNIFUNC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'UFUCODIGOSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de atención de donde se suspende el acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'UFUCODIGOSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'UFUCODIGOSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (sede, hospital, clínica) desde donde se suspende el catéter vascular; FK a ADCENATEN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODCENATESUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad funcional de donde se suspende el acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODCENATESUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODCENATESUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de discontinuación del acceso vascular: flebitis (grados 1-7), infiltración, oclusión, desalojo, disfunción mecánica, cambio a catéter central, cambio por fecha, fin de indicación, hematoma, trombosis u otro; INT FK a tabla de catálogo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'DiscontinuationReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de descontinuación de acceso vascular: 1. Flebitis: Sin dolor, eritema, hinchazón, ni cordón palpable. 2. Flebitis: Dolor en el sitio de inserción del catéter vascular. 3. Flebitis: Dolor en el sitio de inserción del catéter vascular y eritema ligero. 4. Flebitis: Dolor en el sitio de inserción del catéter vascular, eritema y edema ligero. 5. Flebitis: Dolor, eritema, edema con induración mayor a 3 centímetros. 6. Flebitis: Criterio 4, más cordón venoso palpable mayor a 3 centímetros. 7. Flebitis: Salida de material purulento por el sitio de inserción de catéter vascular. 8. Infiltración. 9. Oclusión. 10. Desalojo. 11. Disfunción mecánica. 12. Cambio a catéter central. 13. Cambio por fecha. 14. Fin de la indicación. 15. Hematoma. 16. Proceso trombótico. 17. Otro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'DiscontinuationReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'DiscontinuationReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de punciones venosas realizadas durante el acceso vascular; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NumberOfVenipuncture';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de punciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NumberOfVenipuncture';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NumberOfVenipuncture';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de catéter (periférico, central, PICC, etc.); INT FK a CatheterType', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CatheterTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de catéter de la tabla CatheterType.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CatheterTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CatheterTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del sistema al registrar la suspensión o discontinuación del acceso vascular; DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECREGFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Sistema al Suspender', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECREGFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECREGFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del sistema al crear el registro inicial de venopunción y acceso vascular; DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECREGINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Sistema al crear  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECREGINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECREGINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, enfermero) que registró o ejecutó la suspensión del catéter; VARCHAR FK a INPROFSAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODPROACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud que Suspendio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODPROACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODPROACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones y motivos detallados de la suspensión del acceso vascular: flebitis, infiltración, oclusión, cambio de catéter, fin de indicación u otros; VARCHAR(300)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'MOTITERMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ANTES: Motivo de la suspensión: -Flebitis: Dolor en el Sitio de Venopuncion -Flebitis: Dolor en el Sitio de Venopuncion y Eritema Ligero -Flebitis: Dolor en el Sitio de Venopuncion, Eritema y Edema Ligero -Flebitis: Dolor, Eritema, Edema con Induracion Mayor de 3 Centimetros -Flebitis: Criterios de 4, mas Cordon Venoso Palpable Mayor de 3 Centrimetros -Flebitis: Salida de Material Purulento por el Sitio de Insercion de Cateter -Infiltracion -Disfuncion Mecanica -Cambio a Cateter Central -Cambio por Fecha -Fin de la Indicacion -Hematoma -Otro AHORA: Observaciones de motivo de la suspensión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'MOTITERMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'MOTITERMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización o suspensión del acceso vascular y la venopunción; DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECHAFINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final  de la Venopuncion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECHAFINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECHAFINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio de la venopunción e instalación del catéter vascular; DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECHAINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de la Venopuncion  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECHAINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECHAINIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que señala si el acceso vascular está activo y siendo utilizado en el momento actual (1=activo, 0=inactivo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'ESTACTIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'la Vena esta activa:  Esta Siendo usada en ese Momento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'ESTACTIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'ESTACTIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio o localización anatómica del catéter: extremidades (superior/inferior derecha/izquierda), yugular, subclavia, axilar, femoral, cabeza o cuello; CHAR(2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'EXTREMIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ANTES. Extremidad:  1. Superior Derecha  2. Superior Izquierda  3. Inferior Derecha  4. Inferior Izquierda AHORA. Sitio del catéter: 5. Yugular derecha. 6. Yugular izquierda. 7. Subclavia derecha. 8. Subclavia izquierda. 9. Axilar derecha. 10. Axilar izquierda. 11. Femoral derecha. 12. Femoral izquierda. 13. Miembro superior derecho. 14. Miembro superior izquierdo. 15. Miembro inferior derecho. 16. Miembro inferior izquierdo. 17. Cabeza. 18. Cuello.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'EXTREMIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'EXTREMIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o denominación de la vena donde se inserta el catéter vascular (cefálica, basílica, yugular, femoral, etc.); CHAR(60)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NOMVENUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Vena', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NOMVENUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NOMVENUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación o serial del catéter vascular utilizado; CHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NUMCATETE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Cateter', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NUMCATETE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NUMCATETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre, marca o descripción comercial del catéter vascular (ej: Abbott, B-Braun, Becton Dickinson); CHAR(60)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NOMCATETE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Cateter', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NOMCATETE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NOMCATETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si el catéter está heparinizado (1=sí, 0=no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'ESCATHEPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cateter Heparinizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'ESCATHEPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'ESCATHEPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, enfermero) que registró inicialmente la venopunción e instalación del catéter; VARCHAR FK a INPROFSAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud que registro por primera vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (servicio, centro, departamento) donde se realiza la venopunción; CHAR(10) FK a INUNIFUNC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (sede, hospital, institución) donde se canaliza el acceso vascular; CHAR(10) FK a ADCENATEN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso u hospitalización asociado a la venopunción y acceso vascular del paciente; CHAR(10) FK a ADINGRESO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, identificación, documento identificatorio); VARCHAR(25) PII Identification_Ofuscado; FK a INPACIENT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) del registro de venopunción y acceso vascular en la tabla HCCTRVENP; NUMERIC(18) PK', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Autornumerico Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de catéteres venosos y venopunciones de pacientes hospitalizados. Controla el ciclo de vida de cada acceso venoso periférico o central: tipo de catéter, vena utilizada, extremidad, fechas de inicio y retiro, profesional responsable y motivo de terminación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP';
