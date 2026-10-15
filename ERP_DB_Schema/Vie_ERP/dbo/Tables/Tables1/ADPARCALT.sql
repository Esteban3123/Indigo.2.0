CREATE TABLE [dbo].[ADPARCALT] (
    [CODCENATE] CHAR (10)    NOT NULL,
    [CODINTDIA] CHAR (1)     NOT NULL,
    [NOMDIACAL] CHAR (10)    NOT NULL,
    [CODENTIDA] CHAR (9)     NOT NULL,
    [TRIAGECLA] CHAR (1)     NOT NULL,
    [00]        CHAR (1)     NOT NULL,
    [01]        CHAR (1)     NOT NULL,
    [02]        CHAR (1)     NOT NULL,
    [03]        CHAR (1)     NOT NULL,
    [04]        CHAR (1)     NOT NULL,
    [05]        CHAR (1)     NOT NULL,
    [06]        CHAR (1)     NOT NULL,
    [07]        CHAR (1)     NOT NULL,
    [08]        CHAR (1)     NOT NULL,
    [09]        CHAR (1)     NOT NULL,
    [10]        CHAR (1)     NOT NULL,
    [11]        CHAR (1)     NOT NULL,
    [12]        CHAR (1)     NOT NULL,
    [13]        CHAR (1)     NOT NULL,
    [14]        CHAR (1)     NOT NULL,
    [15]        CHAR (1)     NOT NULL,
    [16]        CHAR (1)     NOT NULL,
    [17]        CHAR (1)     NOT NULL,
    [18]        CHAR (1)     NOT NULL,
    [19]        CHAR (1)     NOT NULL,
    [20]        CHAR (1)     NOT NULL,
    [21]        CHAR (1)     NOT NULL,
    [22]        CHAR (1)     NOT NULL,
    [23]        CHAR (1)     NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_ADPARCALT_1] PRIMARY KEY CLUSTERED ([CODCENATE] ASC, [CODINTDIA] ASC, [CODENTIDA] ASC, [TRIAGECLA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia   [23] = 11 PM ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'23';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia  [22] = 10 PM ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia  [21] = 9 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'21';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia  [20] = 8 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'20';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia  [19] = 7 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'19';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia  [18] = 6 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'18';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia  [17] = 5 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'17';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia  [16] = 4 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'16';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia  [15] = 3 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'15';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia  [14] = 2 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'14';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia [13] = 1 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'13';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia   [12] = 12 PM ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'12';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia   [11] = 11 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia  [10] = 10 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia  [09] = 9 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'09';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia  [08] = 8 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'08';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia   [07] = 7 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'07';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia  [06] = 6 AM ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'06';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia   [05] = 5 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'05';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia   [04] = 4 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'04';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia   [03] = 3 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'03';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia   [02] = 2 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'02';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia  [01] = 1 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'01';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo hora del dia [00] = 12 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'00';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de Clasificacion del TRIAGE    Nota: Solo acepta III o IV', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'TRIAGECLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Dia del Calendario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'NOMDIACAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Dia del Calendario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'CODINTDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Centron de Atencion en donde Ingresa el Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'CODCENATE';

