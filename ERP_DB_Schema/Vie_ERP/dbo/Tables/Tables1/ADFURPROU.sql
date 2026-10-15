CREATE TABLE [dbo].[ADFURPROU] (
    [NUMRADANT]  CHAR (10)                                                                    NULL,
    [RESPGLOSA]  CHAR (1)                                                                     NULL,
    [NUMINGRES]  CHAR (10)                                                                    NOT NULL,
    [NUMFACTUR]  CHAR (20)                                                                    NOT NULL,
    [CODIPSSEC]  CHAR (12)                                                                    NOT NULL,
    [PRIAPEVIC]  CHAR (20)                                                                    NOT NULL,
    [SEGAPEVIC]  CHAR (30)                                                                    NULL,
    [PRINOMVIC]  CHAR (20)                                                                    NOT NULL,
    [SEGNOMVIC]  CHAR (30)                                                                    NULL,
    [TIPDOCVIC]  CHAR (2)                                                                     NOT NULL,
    [NUMDOCVIC]  VARCHAR (25)                                                                 NULL,
    [FECNACVIC]  DATETIME                                                                     NOT NULL,
    [SEXOVICTI]  CHAR (1)                                                                     NOT NULL,
    [DIRRESVIC]  CHAR (40)                                                                    NOT NULL,
    [CODDEPVIC]  CHAR (2)                                                                     NOT NULL,
    [CODMUNVIC]  CHAR (3)                                                                     NOT NULL,
    [NUMTELVIC]  CHAR (10)                                                                    NOT NULL,
    [CODAFIVIC]  CHAR (1)                                                                     NOT NULL,
    [REGACTVIC]  CHAR (1)                                                                     NULL,
    [CODEPSVIC]  CHAR (6)                                                                     NULL,
    [NATUEVENT]  CHAR (2)                                                                     NOT NULL,
    [DESOTREVE]  CHAR (25)                                                                    NULL,
    [DIROCUEVE]  CHAR (40)                                                                    NOT NULL,
    [FECOCUEVE]  DATETIME                                                                     NOT NULL,
    [CODDEPEVE]  CHAR (2)                                                                     NOT NULL,
    [CODMUNEVE]  CHAR (3)                                                                     NOT NULL,
    [CODZONEVE]  CHAR (1)                                                                     NOT NULL,
    [DESBREEVEN] CHAR (255)                                                                   NOT NULL,
    [CODDIAPRI]  CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)') NOT NULL,
    [CODDIAAS1]  CHAR (4)                                                                     NOT NULL,
    [CODDIAAS2]  CHAR (4)                                                                     NOT NULL,
    [CODDIAAS3]  CHAR (4)                                                                     NOT NULL,
    [CODDIAAS4]  CHAR (4)                                                                     NOT NULL,
    [DESPROTES]  CHAR (100)                                                                   NOT NULL,
    [VALRECPRO]  CHAR (15)                                                                    NOT NULL,
    [VALADAPRO]  CHAR (15)                                                                    NOT NULL,
    [VALREAPRO]  CHAR (15)                                                                    NOT NULL,
    [VALTOTREC]  CHAR (15)                                                                    NOT NULL,
    [NUMTOTFOL]  CHAR (3)                                                                     NOT NULL,
    [FECRADFUR]  DATETIME                                                                     NOT NULL,
    CONSTRAINT [PK_ADFURPROU] PRIMARY KEY CLUSTERED ([NUMFACTUR] ASC),
    CONSTRAINT [FK_ADFURPROU_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADFURPROU_INPACIENT] FOREIGN KEY ([NUMDOCVIC]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADFURPROU] NOCHECK CONSTRAINT [FK_ADFURPROU_ADINGRESO];


GO
ALTER TABLE [dbo].[ADFURPROU] NOCHECK CONSTRAINT [FK_ADFURPROU_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha radicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'FECRADFUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMTOTFOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Total Reclamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALTOTREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Reclamado Rehabilitacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALREAPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Reclamado Adaptacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALADAPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Reclamado Protesis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALRECPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Protesis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DESPROTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código asociado 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código asociado 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código asociado 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código asociado 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código diagnóstico principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DESBREEVEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código zona evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODZONEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código municipio evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODMUNEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo departamento evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDEPEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Evento Accidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'FECOCUEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Direccion Evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DIROCUEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otro Evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DESOTREVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza Evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NATUEVENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Eps', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODEPSVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Regimen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'REGACTVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Afiliacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODAFIVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'número de teléfono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMTELVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODMUNVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDEPVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DIRRESVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'sexo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'SEXOVICTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'FECNACVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'número de documento ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMDOCVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tipo identificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'TIPDOCVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'segundo nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'SEGNOMVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'PRINOMVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'SEGAPEVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'PRIAPEVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la IPS ante la Secretaria Departamental de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODIPSSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMFACTUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'RESPGLOSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No radicado anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMRADANT';

