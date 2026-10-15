CREATE TABLE [dbo].[HCSESHEM] (
    [ID]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [CODCENATE]       CHAR (10)                                                                        NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NULL,
    [NUMSILLA]        VARCHAR (20)                                                                     NULL,
    [PESOSECO]        NUMERIC (18, 2)                                                                  NULL,
    [PESOPREDIALISIS] NUMERIC (18, 2)                                                                  NULL,
    [ACCESOVASCULAR]  TINYINT                                                                          NULL,
    [FILTRO]          NUMERIC (18, 2)                                                                  NULL,
    [ULTRAFILTRACION] NUMERIC (18, 2)                                                                  NULL,
    [HEPARINA]        NUMERIC (18, 2)                                                                  NULL,
    [NOTENFINIID]     INT                                                                              NULL,
    [FECINISES]       DATETIME                                                                         NULL,
    [USUINISES]       CHAR (20)                                                                        NULL,
    [PESOPOST]        NUMERIC (18, 2)                                                                  NULL,
    [VOLSANGTRA]      NUMERIC (18, 2)                                                                  NULL,
    [NOTENFFINID]     INT                                                                              NULL,
    [FECFINSES]       DATETIME                                                                         NULL,
    [USUFINSES]       CHAR (20)                                                                        NULL,
    [FECCREREG]       DATETIME                                                                         NULL,
    [FECMODREG]       DATETIME                                                                         NULL,
    [HORASES]         INT                                                                              NULL,
    [MINSES]          INT                                                                              NULL,
    [ESTADO]          INT                                                                              NULL,
    [HOMGLU]          INT                                                                              NULL,
    [NUMEROFISTULA]   INT                                                                              NULL,
    CONSTRAINT [PK_HCSESHEM] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCSESHEM_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCSESHEM_HCCTRNOTE] FOREIGN KEY ([NOTENFFINID]) REFERENCES [dbo].[HCCTRNOTE] ([ID]),
    CONSTRAINT [FK_HCSESHEM_HCCTRNOTE1] FOREIGN KEY ([NOTENFINIID]) REFERENCES [dbo].[HCCTRNOTE] ([ID]),
    CONSTRAINT [FK_HCSESHEM_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCSESHEM] NOCHECK CONSTRAINT [FK_HCSESHEM_ADINGRESO];


GO
ALTER TABLE [dbo].[HCSESHEM] NOCHECK CONSTRAINT [FK_HCSESHEM_HCCTRNOTE];


GO
ALTER TABLE [dbo].[HCSESHEM] NOCHECK CONSTRAINT [FK_HCSESHEM_HCCTRNOTE1];


GO
ALTER TABLE [dbo].[HCSESHEM] NOCHECK CONSTRAINT [FK_HCSESHEM_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la cantidad de Fistula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NUMEROFISTULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la cantidad de Glucomatria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'HOMGLU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de Sesion 1 - Proceso 2- Finalizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Minutos de Sesion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'MINSES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora  de Sesion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'HORASES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECMODREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECCREREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que finaliza la sesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'USUFINSES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de fin de sesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECFINSES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la nota de enfermería de fin de sesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NOTENFFINID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bolsa de sangre tratada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'VOLSANGTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'peso post', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'PESOPOST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que inicia la sesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'USUINISES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de inicio de sesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECINISES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la nota de enfermería de inicio de sesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NOTENFINIID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Heparina  Unidad: ui  desde 0 -10000', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'HEPARINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ultra Filtración   Unidad:ml  desde 0 - 9999', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ULTRAFILTRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Filtro, decimal desde 0 hasta 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FILTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acceso Vascular:  1-> Cateter  2-> Fístula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ACCESOVASCULAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pero predialisis  "validación de peso en gr o kr segun edad, <= 3 meses gr" 3 digitos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'PESOPREDIALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso seco, se trae de la historia clínica y no se permite modificar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'PESOSECO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de silla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NUMSILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ID';

