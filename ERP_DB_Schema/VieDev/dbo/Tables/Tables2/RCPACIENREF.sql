CREATE TABLE [dbo].[RCPACIENREF] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IPTIPODOC] INT                                                                              NOT NULL,
    [IPPRINOMB] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')         NOT NULL,
    [IPSEGNOMB] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')        NULL,
    [IPPRIAPEL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')      NOT NULL,
    [IPSEGAPEL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')     NULL,
    [IPNOMCOMP] CHAR (250) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')             NOT NULL,
    [IPFECNACI] DATE MASKED WITH (FUNCTION = 'default()')                                        NOT NULL,
    [IPSEXOPAC] INT                                                                              NOT NULL,
    [CODENTIDA] CHAR (9)                                                                         NULL,
    [AUUBICACI] CHAR (20)                                                                        NOT NULL,
    [IPDIRECCI] VARCHAR (MAX) MASKED WITH (FUNCTION = 'default()')                               NULL,
    [IPTELEFON] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)')         NULL,
    CONSTRAINT [PK_RCPacienRef] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC),
    CONSTRAINT [FK_RCPACIENREF_INUBICACI] FOREIGN KEY ([AUUBICACI]) REFERENCES [dbo].[INUBICACI] ([AUUBICACI])
);


GO
ALTER TABLE [dbo].[RCPACIENREF] NOCHECK CONSTRAINT [FK_RCPACIENREF_INUBICACI];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo del Paciente:  1=Masculino   2=Femenino     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPSEXOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Documento  1=Cédula de Ciudadanía  2=Cédula de Extranjería  3=Tarjeta de Identidad  4=Registro Civil  5=Pasporte  6=Adulto Sin Identificación  7=Menor Sin Identificación  8=Número único de identificación personal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPTIPODOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPCODPACI';

