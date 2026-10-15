CREATE TABLE [dbo].[ADFURTRAN] (
    [NUMRADANT] CHAR (10)    NULL,
    [RESPGLOSA] CHAR (1)     NULL,
    [NUMINGRES] CHAR (10)    NOT NULL,
    [FECRADFOR] DATETIME     NOT NULL,
    [NOMEMPTRA] CHAR (60)    NOT NULL,
    [CODIPSSEC] CHAR (12)    NOT NULL,
    [PRIAPEREC] CHAR (20)    NOT NULL,
    [SEGAPEREC] CHAR (30)    NULL,
    [PRINOMREC] CHAR (20)    NOT NULL,
    [SEGNOMREC] CHAR (30)    NULL,
    [TIPDOCREC] CHAR (2)     NOT NULL,
    [NUMDOCREC] VARCHAR (25) NOT NULL,
    [TIPVEHREC] CHAR (1)     NOT NULL,
    [OTRVEHREC] CHAR (20)    NULL,
    [PLAVEHICU] CHAR (6)     NULL,
    [DIRRESREC] CHAR (40)    NOT NULL,
    [NUMTELREC] CHAR (10)    NOT NULL,
    [CODDEPREC] CHAR (2)     NOT NULL,
    [CODMUNREC] CHAR (3)     NOT NULL,
    [TIPDOCVIC] CHAR (2)     NOT NULL,
    [NUMDOCVIC] VARCHAR (25) NULL,
    [PRINOMVIC] CHAR (20)    NOT NULL,
    [SEGNOMVIC] CHAR (30)    NULL,
    [PRIAPEVIC] CHAR (20)    NOT NULL,
    [SEGAPEVIC] CHAR (30)    NULL,
    [TIPEVEMOV] CHAR (1)     NOT NULL,
    [DIRRECVIC] CHAR (40)    NOT NULL,
    [CODDEPRVI] CHAR (2)     NOT NULL,
    [CODMUNRVI] CHAR (3)     NOT NULL,
    [ZONRECVIC] CHAR (1)     NOT NULL,
    [FECTRAVIC] DATETIME     NOT NULL,
    [HORTRAVIC] CHAR (5)     NOT NULL,
    [CODHABIPS] CHAR (12)    NOT NULL,
    [CODDEREVI] CHAR (2)     NOT NULL,
    [CODMUREVI] CHAR (3)     NOT NULL,
    [NUMTOTFOL] CHAR (3)     NOT NULL,
    [ID]        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_ADFURTRAN] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADFURTRAN_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADFURTRAN_INPACIENT] FOREIGN KEY ([NUMDOCVIC]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADFURTRAN] NOCHECK CONSTRAINT [FK_ADFURTRAN_ADINGRESO];


GO
ALTER TABLE [dbo].[ADFURTRAN] NOCHECK CONSTRAINT [FK_ADFURTRAN_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda cosecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda numero folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMTOTFOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código del municipio de certificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODMUREVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código del departamento de certificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODDEREVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la IPS ante la Secretaria Departamental de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODHABIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de Traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'HORTRAVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'FECTRAVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Zona Evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'ZONRECVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código del municipio donde se recoge la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODMUNRVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código del departamento donde se recoge la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODDEPRVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'direccion de residencia de la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'DIRRECVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza Evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPEVEMOV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'segundo apellido de la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGAPEVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'primer apellido de la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRIAPEVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'segundo nombre de la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGNOMVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'primer nombre de la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRINOMVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de documento de la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMDOCVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Identificacion de la Victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPDOCVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código del municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODMUNREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código de departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODDEPREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'número telefono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMTELREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'dirección de residencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'DIRRESREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'placa del vehículo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PLAVEHICU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otro tipo de vehículo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'OTRVEHREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tipo de vehículo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPVEHREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'número de documento reclamante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMDOCREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Identificacion Reclamante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPDOCREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'segundo nombre reclamante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGNOMREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'primer nombre reclamante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRINOMREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo apellido reclamante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGAPEREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer apellido reclamante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRIAPEREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la IPS ante la Secretaria Departamental de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODIPSSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NOMEMPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de radicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'FECRADFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'número de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'RESPGLOSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número radicado anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMRADANT';

