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
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_ADCENATEN];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_ADCENATEN1];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_CatheterType];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_INPACIENT];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_INPROFSAL];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_INPROFSAL1];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_INUNIFUNC1];


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRVENP__IPCODPACI__NUMINGRES__NOMVENUTI]
    ON [dbo].[HCCTRVENP]([IPCODPACI] ASC, [NUMINGRES] ASC, [NOMVENUTI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRVENB]
    ON [dbo].[HCCTRVENP]([NUMINGRES] ASC, [IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de atención de donde se suspende el acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'UFUCODIGOSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad funcional de donde se suspende el acceso vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODCENATESUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de descontinuación de acceso vascular: 1. Flebitis: Sin dolor, eritema, hinchazón, ni cordón palpable. 2. Flebitis: Dolor en el sitio de inserción del catéter vascular. 3. Flebitis: Dolor en el sitio de inserción del catéter vascular y eritema ligero. 4. Flebitis: Dolor en el sitio de inserción del catéter vascular, eritema y edema ligero. 5. Flebitis: Dolor, eritema, edema con induración mayor a 3 centímetros. 6. Flebitis: Criterio 4, más cordón venoso palpable mayor a 3 centímetros. 7. Flebitis: Salida de material purulento por el sitio de inserción de catéter vascular. 8. Infiltración. 9. Oclusión. 10. Desalojo. 11. Disfunción mecánica. 12. Cambio a catéter central. 13. Cambio por fecha. 14. Fin de la indicación. 15. Hematoma. 16. Proceso trombótico. 17. Otro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'DiscontinuationReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de punciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NumberOfVenipuncture';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de catéter de la tabla CatheterType.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CatheterTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Sistema al Suspender', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECREGFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Sistema al crear  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECREGINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud que Suspendio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODPROACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ANTES: Motivo de la suspensión: -Flebitis: Dolor en el Sitio de Venopuncion -Flebitis: Dolor en el Sitio de Venopuncion y Eritema Ligero -Flebitis: Dolor en el Sitio de Venopuncion, Eritema y Edema Ligero -Flebitis: Dolor, Eritema, Edema con Induracion Mayor de 3 Centimetros -Flebitis: Criterios de 4, mas Cordon Venoso Palpable Mayor de 3 Centrimetros -Flebitis: Salida de Material Purulento por el Sitio de Insercion de Cateter -Infiltracion -Disfuncion Mecanica -Cambio a Cateter Central -Cambio por Fecha -Fin de la Indicacion -Hematoma -Otro AHORA: Observaciones de motivo de la suspensión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'MOTITERMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final  de la Venopuncion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECHAFINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de la Venopuncion  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECHAINIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'la Vena esta activa:  Esta Siendo usada en ese Momento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'ESTACTIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ANTES. Extremidad:  1. Superior Derecha  2. Superior Izquierda  3. Inferior Derecha  4. Inferior Izquierda AHORA. Sitio del catéter: 5. Yugular derecha. 6. Yugular izquierda. 7. Subclavia derecha. 8. Subclavia izquierda. 9. Axilar derecha. 10. Axilar izquierda. 11. Femoral derecha. 12. Femoral izquierda. 13. Miembro superior derecho. 14. Miembro superior izquierdo. 15. Miembro inferior derecho. 16. Miembro inferior izquierdo. 17. Cabeza. 18. Cuello.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'EXTREMIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Vena', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NOMVENUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Cateter', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NUMCATETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Cateter', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NOMCATETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cateter Heparinizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'ESCATHEPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud que registro por primera vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Autornumerico Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'CONSECUTI';

