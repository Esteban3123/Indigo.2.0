CREATE TABLE [dbo].[INESCALADT] (
    [NUMINGRES]         CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [EDADPUNTAJE]       TINYINT                                                                          NULL,
    [EDADSELECCION]     TINYINT                                                                          NULL,
    [FECHAREGIS]        DATETIME                                                                         NOT NULL,
    [CAIDPREVIA]        TINYINT                                                                          NOT NULL,
    [TRANQUILIZANTES]   TINYINT                                                                          NOT NULL,
    [DIURETICOS]        TINYINT                                                                          NOT NULL,
    [HIPOTENSORES]      TINYINT                                                                          NOT NULL,
    [ANTIPARKINSON]     TINYINT                                                                          NOT NULL,
    [ANTIDESPRESIVO]    TINYINT                                                                          NOT NULL,
    [OTROMEDICAMEN]     TINYINT                                                                          NOT NULL,
    [MEDICAMENTOS]      VARCHAR (MAX)                                                                    NULL,
    [ALTERACIONVISUAL]  TINYINT                                                                          NOT NULL,
    [ALTERACIONAUDIT]   TINYINT                                                                          NOT NULL,
    [ICTUSEXTREMIDAD]   TINYINT                                                                          NOT NULL,
    [ESTADOMENTAL]      TINYINT                                                                          NOT NULL,
    [SEGURAAYUDA]       TINYINT                                                                          NOT NULL,
    [INSEGURAAYUDA]     TINYINT                                                                          NOT NULL,
    [IMPOSIBLE]         TINYINT                                                                          NOT NULL,
    [PATOLOGIA]         TINYINT                                                                          NOT NULL,
    [ESTADONUTRICIONAL] TINYINT                                                                          NOT NULL,
    CONSTRAINT [PK_INESCALADT_1] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC),
    CONSTRAINT [FK_INESCALADT_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_INESCALADT_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[INESCALADT] NOCHECK CONSTRAINT [FK_INESCALADT_ADINGRESO];


GO
ALTER TABLE [dbo].[INESCALADT] NOCHECK CONSTRAINT [FK_INESCALADT_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado nutricional 1=adecuado  0=inadecuado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ESTADONUTRICIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'patologías 1=No asiciada a caidas  2=asociada a acidas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'PATOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deambulacion imposible 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'IMPOSIBLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deambulacion insegura con ayuda/sin ayuda 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'INSEGURAAYUDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deambulacion segura con ayuda 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'SEGURAAYUDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado mental 1=orientado 2=confuso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ESTADOMENTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ictus en extremidades 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ICTUSEXTREMIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Alteración auditiva 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ALTERACIONAUDIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Alteración visual 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ALTERACIONVISUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otro medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'OTROMEDICAMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antidepresivos 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ANTIDESPRESIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'AntiParkinson 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ANTIPARKINSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hipotensores 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'HIPOTENSORES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diureticos 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'DIURETICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tranquilizantes 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'TRANQUILIZANTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Caídas previas 1=si  2=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'CAIDPREVIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'FECHAREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad seleccionada: 0: de 5 a 59 años, 1: menor de 5 años, 2: Mayor de 60 años , ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'EDADSELECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Puntaje: 1 o 0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'EDADPUNTAJE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';

