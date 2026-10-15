CREATE TABLE [dbo].[HCPLANVAC] (
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IDPLAVACU]    CHAR (4)                                                                         NOT NULL,
    [FECAPLVAC]    DATETIME                                                                         NULL,
    [OBSVACUNA]    CHAR (200)                                                                       NULL,
    [INDAUDFOR]    NUMERIC (18)                                                                     NOT NULL,
    [ID]           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MTVNOAPL]     TINYINT                                                                          NULL,
    [CODCENATE]    CHAR (10)                                                                        NULL,
    [CODENTIDA]    CHAR (9)                                                                         NULL,
    [GENCONENTITY] INT                                                                              NULL,
    [LOTEDOSIS]    VARCHAR (20)                                                                     NULL,
    [LOTEJERINGA]  VARCHAR (20)                                                                     NULL,
    [NUMFOLIO]     NCHAR (10)                                                                       NULL,
    [USUREGISTRA]  VARCHAR (80)                                                                     NULL,
    [ADMOTRINS]    BIT                                                                              NULL,
    [APLICADA]     BIT                                                                              NULL,
    [NUMINGRES]    CHAR (10)                                                                        NULL,
    CONSTRAINT [PK_HCPLANVAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPLANVAC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPLANVAC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPLANVAC_INENTADM] FOREIGN KEY ([CODENTIDA]) REFERENCES [dbo].[INENTADM] ([CODENTADM]),
    CONSTRAINT [FK_HCPLANVAC_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPLANVAC_INPLANVAC] FOREIGN KEY ([IDPLAVACU]) REFERENCES [dbo].[INPLANVAC] ([IDPLAVACU])
);


GO
ALTER TABLE [dbo].[HCPLANVAC] NOCHECK CONSTRAINT [FK_HCPLANVAC_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPLANVAC] NOCHECK CONSTRAINT [FK_HCPLANVAC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPLANVAC] NOCHECK CONSTRAINT [FK_HCPLANVAC_INENTADM];


GO
ALTER TABLE [dbo].[HCPLANVAC] NOCHECK CONSTRAINT [FK_HCPLANVAC_INPacient];


GO
ALTER TABLE [dbo].[HCPLANVAC] NOCHECK CONSTRAINT [FK_HCPLANVAC_INPLANVAC];


GO
CREATE NONCLUSTERED INDEX [IX_HCPLANVAC__IPCODPACI]
    ON [dbo].[HCPLANVAC]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que registro la vacuna  Se llena unicamente cuando  se Aplica la vacuna Mediante una evolucion en el control de Antecedentes.  Cuando queda el campo Vacio es porque lo hicieron esde el Formulario de Vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'USUREGISTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Folio Con el que se guardo la aplicacion de la vacuna  Se llena unicamente cuando  se Aplica la vacuna Mediante una evolucion en el control de Antecedentes.  Cuando queda el campo Vacio es porque lo hicieron esde el Formulario de Vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'NUMFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo no Aplicación  0- Aplicada  1- No se Administra por una Tradición,   2- No se Administra por una Condición de Salud,  3- No se Administra por Negación del Usuario,  4- No se Administra por tener datos del contacto del usuario no actualizado,   5- No se administra por otras razones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'MTVNOAPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CAmpo Auditoria  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';

