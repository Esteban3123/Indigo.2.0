CREATE TABLE [dbo].[HCFICHA342] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [NIVELEDUCA]          INT           NULL,
    [TRABURBAN]           BIT           NULL,
    [TRABRURAL]           BIT           NULL,
    [JOVENVULNE]          BIT           NULL,
    [JOVENVULURB]         BIT           NULL,
    [DISSISNERV]          BIT           NULL,
    [DISOJOS]             BIT           NULL,
    [DISOIDOS]            BIT           NULL,
    [DISDEMAS]            BIT           NULL,
    [DISVOZ]              BIT           NULL,
    [DISSISTEM]           BIT           NULL,
    [DISDIGEST]           BIT           NULL,
    [DISSISTGENI]         BIT           NULL,
    [DISMOVI]             BIT           NULL,
    [DISPIEL]             BIT           NULL,
    [DISOTRO]             BIT           NULL,
    [NODEFIN]             BIT           NULL,
    [FECHADIAG]           DATE          NULL,
    [PRUEBLAB]            VARCHAR (50)  NULL,
    [NOMBENFER]           VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA342] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA342_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA342_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA342] NOCHECK CONSTRAINT [CK_HCFICHA342_JSON];


GO
ALTER TABLE [dbo].[HCFICHA342] NOCHECK CONSTRAINT [FK_HCFICHA342_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nodefin:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'NODEFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disotro:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dispiel:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISPIEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dismovi:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISMOVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dissistgeni:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISSISTGENI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disdigest:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISDIGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dissistem:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISSISTEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disvoz:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISVOZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disdemas:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISDEMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disoidos:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISOIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disojos:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISOJOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dissisnerv:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'DISSISNERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Jovenvulurb:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'JOVENVULURB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Jovenvulne:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'JOVENVULNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Trabrural:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'TRABRURAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Traburban:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'TRABURBAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel educativo:  1=Preescolar   2=Básica Primaria   3=Básica Secundaria   4=Media Académica o Clásica   5=Media Técnica (Bachillerato Técnico)   6=Normalista   7=Técnica Profesional   8=Tecnológica   9=Profesional   10=Especialización   11=Maestría   12=Doctorado   13=Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA342', @level2type = N'COLUMN', @level2name = N'NIVELEDUCA';

