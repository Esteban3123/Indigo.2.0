CREATE TABLE [dbo].[HCPRESCRD] (
    [CODCONCEC]                     NUMERIC (18)                                                                     NOT NULL,
    [IDETIPHIS]                     CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                     NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                     CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                     CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPRODUC]                     CHAR (20)                                                                        NOT NULL,
    [CODVIAADM]                     VARCHAR (20)                                                                     NOT NULL,
    [CODFORMED]                     VARCHAR (20)                                                                     NOT NULL,
    [DOSISPROD]                     NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMED]                     VARCHAR (20)                                                                     NULL,
    [FRECUENCI]                     INT                                                                              NULL,
    [UNIFRECUE]                     CHAR (1)                                                                         NULL,
    [FECINIDOS]                     DATETIME                                                                         NOT NULL,
    [TIPFORMED]                     CHAR (1)                                                                         NOT NULL,
    [DURACIDOS]                     CHAR (20)                                                                        NOT NULL,
    [VALDURFIJ]                     INT                                                                              NULL,
    [UNIDURFIJ]                     CHAR (1)                                                                         NULL,
    [FECFINDOS]                     DATETIME                                                                         NULL,
    [CANPEDPRO]                     INT                                                                              NOT NULL,
    [PREFECANT]                     BIT                                                                              NOT NULL,
    [MOTPREANT]                     CHAR (250)                                                                       NULL,
    [PREESTADO]                     INT                                                                              NOT NULL,
    [INDAPLMED]                     VARCHAR (MAX)                                                                    NULL,
    [MANEXTPRO]                     BIT                                                                              NOT NULL,
    [FOLIOINIC]                     NCHAR (10)                                                                       NOT NULL,
    [TRATMODIF]                     BIT                                                                              NOT NULL,
    [FORMUMANU]                     BIT                                                                              NULL,
    [DESADMINI]                     VARCHAR (MAX)                                                                    NULL,
    [INDAUDFOR]                     NUMERIC (18)                                                                     NOT NULL,
    [DOSISPRFN]                     NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMFN]                     VARCHAR (20)                                                                     NULL,
    [OBSJUSMEE]                     VARCHAR (MAX)                                                                    NULL,
    [CODJUMEES]                     CHAR (3)                                                                         NULL,
    [IDESQUEMAONC]                  INT                                                                              NULL,
    [INCONSECUCONTROL]              INT                                                                              NULL,
    [FORMAPRESCRIBE]                TINYINT                                                                          NULL,
    [NUMERODOSIS]                   TINYINT                                                                          NULL,
    [DOSISPROD1]                    NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD2]                    NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD3]                    NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD4]                    NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD5]                    NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD6]                    NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD7]                    NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD8]                    NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD9]                    NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD10]                   NUMERIC (18, 2)                                                                  NULL,
    [HORA1]                         DATETIME                                                                         NULL,
    [HORA2]                         DATETIME                                                                         NULL,
    [HORA3]                         DATETIME                                                                         NULL,
    [HORA4]                         DATETIME                                                                         NULL,
    [HORA5]                         DATETIME                                                                         NULL,
    [HORA6]                         DATETIME                                                                         NULL,
    [HORA7]                         DATETIME                                                                         NULL,
    [HORA8]                         DATETIME                                                                         NULL,
    [HORA9]                         DATETIME                                                                         NULL,
    [HORA10]                        DATETIME                                                                         NULL,
    [JUSTIFICARB]                   VARCHAR (500)                                                                    NULL,
    [JUSTIFICACIONPBS]              VARCHAR (2000)                                                                   NULL,
    [IDHCORDQUIMIO]                 INT                                                                              NULL,
    [ID]                            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TraceabilityPaperworkEventsId] INT                                                                              NULL,
    [TraceabilityPaperworkId]       INT                                                                              NULL,
    [Principal] BIT NULL, 
    CONSTRAINT [PK__HCPRESCR__3214EC27DA81EE48] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPRESCRD_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPRESCRD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPRESCRD].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPRESCRD].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO

CREATE NONCLUSTERED INDEX [IX_HCPRESCRD_IPCODPACI_NUMINGRES_MANEXTPRO_CANPEDPRO_CODPRODUC_CODUNIMED_CODVIAADM_DOSISPROD_DURACIDOS_FOLIOINIC_FRECUENCI]
    ON [dbo].[HCPRESCRD]([IPCODPACI] ASC, [NUMINGRES] ASC, [MANEXTPRO] ASC)
    INCLUDE([CANPEDPRO], [CODPRODUC], [CODUNIMED], [CODVIAADM], [DOSISPROD], [DURACIDOS], [FOLIOINIC], [FRECUENCI], [INDAPLMED], [NUMEFOLIO], [TRATMODIF], [UNIFRECUE]);


GO
CREATE NONCLUSTERED INDEX [IDX_HCPRESCRD_IPCODPACI_INCONSECUCONTROL]
    ON [dbo].[HCPRESCRD]([IPCODPACI] ASC, [INCONSECUCONTROL] ASC)
    INCLUDE([CODPRODUC], [NUMEFOLIO], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_HCPRESCRD_NUMINGRES]
    ON [dbo].[HCPRESCRD]([NUMINGRES] ASC);


GO
ALTER INDEX [IX_HCPRESCRD_NUMINGRES]
    ON [dbo].[HCPRESCRD] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCPRESCRD]
    ON [dbo].[HCPRESCRD]([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [MANEXTPRO] ASC)
    INCLUDE([CANPEDPRO], [CODPRODUC], [CODUNIMED], [DOSISPROD], [DURACIDOS], [FRECUENCI], [INDAPLMED]);


GO
CREATE NONCLUSTERED INDEX [IX_HCPRESCRD_CODCONCEC_CODPRODUC_MANEXTPRO_IDESQUEMAONC]
    ON [dbo].[HCPRESCRD]([CODCONCEC] ASC, [CODPRODUC] ASC, [MANEXTPRO] ASC, [IDESQUEMAONC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de trazabilidad del trámite/documentación de la prescripción para auditoría y seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la trazabilidad del pepeleo ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de trazabilidad de eventos asociados al trámite de la prescripción para auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la trazabilidad eventos ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (identity) del registro de prescripción, clave primaria de la tabla HCPRESCRD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'gurada el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a orden de quimioterapia oncológica; vincula prescripciones de medicamentos quimioterápicos con su esquema de tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de Orden de Quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica para medicamento PBS (Programa de Beneficiarios en Salud); texto VARCHAR(2000) con argumentos médicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación clínica PBS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica para medicamento con resistencia bacteriana; texto VARCHAR(500) que respalda uso ante resistencia antimicrobiana.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'JUSTIFICARB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación de medicamento con resistencia bacteriana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'JUSTIFICARB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'JUSTIFICARB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora datetime de administración de la décima dosis del medicamento prescrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la decima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora datetime de administración de la novena dosis del medicamento prescrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la novena dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora datetime de administración de la octava dosis del medicamento prescrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la octava dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora datetime de administración de la séptima dosis del medicamento prescrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la septima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora datetime de administración de la sexta dosis del medicamento prescrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la sexta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora datetime de administración de la quinta dosis del medicamento prescrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la quinta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora datetime de administración de la cuarta dosis del medicamento prescrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la cuarta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora datetime de administración de la tercera dosis del medicamento prescrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la tercera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora datetime de administración de la segunda dosis del medicamento prescrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la segunda dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora datetime de administración de la primera dosis del medicamento prescrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la primera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'HORA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Décima dosis: cantidad numeric(18,2) en unidad de medida específica del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Decima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Novena dosis: cantidad numeric(18,2) en unidad de medida específica del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Novena dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Octava dosis: cantidad numeric(18,2) en unidad de medida específica del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Octava dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Séptima dosis: cantidad numeric(18,2) en unidad de medida específica del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Septima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexta dosis: cantidad numeric(18,2) en unidad de medida específica del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quinta dosis: cantidad numeric(18,2) en unidad de medida específica del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Quinta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuarta dosis: cantidad numeric(18,2) en unidad de medida específica del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuarta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercera dosis: cantidad numeric(18,2) en unidad de medida específica del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda dosis: cantidad numeric(18,2) en unidad de medida específica del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segunda dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera dosis: cantidad numeric(18,2) en unidad de medida específica del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de dosis a administrar (tinyint); define cantidad de aplicaciones del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma de prescripción: 0=Estándar (catálogo), 1=Personalizada (ajustada al paciente); tinyint.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Forma de prescripción: 0:Estandar, 1:Personalizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de medicamentos de control (sustancias reguladas); int para auditoría de prescripción controlada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'INCONSECUCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me va a guardar el conscutivo de los medicamentos que son de control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'INCONSECUCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'INCONSECUCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema oncológico (quimioterapia/radioterapia); int, vincula con orden de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'IDESQUEMAONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id esquema oncologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'IDESQUEMAONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'IDESQUEMAONC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de justificación de medicamento especial; char(3) referencia a catálogo de justificaciones clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la justificacion de meticamento especial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODJUMEES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de justificación de medicamento especial; varchar(max) con argumentos médicos detallados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'OBSJUSMEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de la justificacion de medicamentos especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'OBSJUSMEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'OBSJUSMEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad de medida final después de conversión (gramos, miligramos, microgramos); varchar(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODUNIMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para almacenar la Unidad Medida final cuando hay conversiones entre (Gramos , Miligramos , Microgramos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODUNIMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODUNIMFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis final después de conversión entre unidades de peso/volumen; numeric(18,2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPRFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para almacenar la dosis final cuando hay conversiones entre (Gramos , Miligramos , Microgramos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPRFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPRFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría de fórmula; numeric(18) para control de cambios y trazabilidad normativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de administración del medicamento; varchar(max) con instrucciones al personal de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Administracion del medicamento ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DESADMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si formulación fue manual (0=no, 1=sí); bit, marca prescripciones no automatizadas del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FORMUMANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulacion Manual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FORMUMANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FORMUMANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si medicamento fue modificado en la orden médica (0=original, 1=modificado); bit para auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'TRATMODIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si es Un Medicamento que fue Modificado en la Orden Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'TRATMODIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'TRATMODIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del folio inicial donde se prescribió originalmente; nchar(10), referencia a prescripción previa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FOLIOINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio inicial donde se prescribio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FOLIOINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FOLIOINIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si producto corresponde a plan de manejo externo/extramural (0=no, 1=sí); bit.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este Producto Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones de aplicación de medicamentos; varchar(max) con instrucciones específicas de administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones de Aplicacion de Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'INDAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de prescripción: 1=Interfase inventarios, 2=Sin interfase (existencia kardex), 3=Sin interfase (no existe), 4=Extramural; int.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'PREESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Medicamento  1: Realiza Interfase con Inventarios  2: No Realiza Interfase con Inventarios x existencia del producto en el Kardex del Paciente correspondiente a la Centro de Atencion y Unidad Funcional Actual  3: No Realiza Interfase con Inventarios x no existencia del producto en el Kardez del modulo de Inventario.  4: Manejo Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'PREESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'PREESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de prescripción anterior/pasada; varchar(250) justificación de por qué cambió o se repitió.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'MOTPREANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo Prescripcion Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'MOTPREANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'MOTPREANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si prescripción ocurrió en fecha anterior a hoy (0=no, 1=sí); bit, marca prescripciones retrospectivas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'PREFECANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la Prescripcion ocurrio en una Fecha Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'PREFECANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'PREFECANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pedida del producto; int, cantidad total de unidades a suministrar en el ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pedida del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de la última dosis; datetime, marca fin del tratamiento medicamentoso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FECFINDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final de la Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FECFINDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FECFINDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de duración fija: 1=Minutos, 2=Horas, 3=Días, 4=Semanas, 5=Meses, 6=Años; char(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias  4: Semanas  5: Meses  6: Años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de duración fija; int, número de unidades (ej: 7 días, 2 semanas).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la duracion fija', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración de la dosis; char(20), especifica cuánto tiempo cubre el tratamiento prescrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion de la Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de formulación: 1=Peso (mg), 2=Volumen (ml), 3=Peso-Volumen, 4=Unidad de administración; char(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial de la dosis; datetime, marca inicio del tratamiento medicamentoso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FECINIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FECINIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FECINIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de frecuencia: 1=Minutos, 2=Horas, 3=Días; char(1), define periodicidad de aplicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia de administración; int, número de veces por unidad de tiempo (ej: 3 veces al día).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'FRECUENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad de medida del medicamento; varchar(20), referencia a catálogo (mg, ml, u, etc).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis del producto; numeric(18,2), cantidad por administración en la unidad de medida especificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de forma de presentación del medicamento; varchar(20), referencia a tableta, cápsula, solución, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Forma de presentacion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de vía de administración común; varchar(20), referencia a oral, IV, IM, SC, inhalatoria, tópica, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto/medicamento; char(20), identificador del producto farmacéutico del catálogo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud prescriptor; varchar(20) MASKED, PII, identificación del médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional; char(10), referencia a farmacia, urgencia, UCI, piso, consulta externa, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención; char(10), referencia a hospital, clínica o sitio de servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso del paciente; char(10), agrupa prescripciones de una atención/hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, documento, identificación); varchar(25) MASKED PII, FK a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de la prescripción; nchar(10), identificador único del formulario/documento médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno tipo de historia clínica; char(9), clasificación de tipo de expediente del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo; numeric(18), número correlativo de prescripción dentro de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Indica cual es el medicamento principal de la atención - Solo aplica para unidad de urgencias',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'HCPRESCRD',
    @level2type = N'COLUMN',
    @level2name = N'Principal'


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prescripciones de medicamentos registradas en la historia clínica. Guarda cada línea de medicamento prescrito a un paciente durante un ingreso, incluyendo producto, dosis, vía de administración, frecuencia, duración del tratamiento y estado de la prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRD';
