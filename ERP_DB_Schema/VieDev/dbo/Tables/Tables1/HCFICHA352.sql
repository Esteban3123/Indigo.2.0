CREATE TABLE [dbo].[HCFICHA352] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [COMPLESERVI]         INT           NULL,
    [SERVIADMIN]          INT           NULL,
    [PROCEDMEDI]          INT           NULL,
    [DIABETES]            BIT           NULL,
    [INMUNOSUPRE]         BIT           NULL,
    [OBESIDAD]            BIT           NULL,
    [DESNUTRICION]        BIT           NULL,
    [PREECLAMPSIA]        BIT           NULL,
    [ANEMIA]              BIT           NULL,
    [CLASIFASA]           INT           NULL,
    [TIPOHERIDA]          INT           NULL,
    [DURAPROCE]           INT           NULL,
    [SUPERPRIMA]          BIT           NULL,
    [SUPERSECUN]          BIT           NULL,
    [PROFUPRIMA]          BIT           NULL,
    [PROFUSECUN]          BIT           NULL,
    [ORGAESPAC]           BIT           NULL,
    [PROFILAXIS]          BIT           NULL,
    [TIEMPOADMIN]         INT           NULL,
    [LAPAROSCOPIA]        BIT           NULL,
    [FECHATOMA1]          DATE          NULL,
    [FECHATOMA2]          DATE          NULL,
    [FECHATOMA3]          DATE          NULL,
    [CODMUESTRA1]         INT           NULL,
    [CODMUESTRA2]         INT           NULL,
    [CODMUESTRA3]         INT           NULL,
    [CODPRUEBA1]          INT           NULL,
    [CODPRUEBA2]          INT           NULL,
    [CODPRUEBA3]          INT           NULL,
    [MICROORGA1]          VARCHAR (50)  NULL,
    [MICROORGA2]          VARCHAR (50)  NULL,
    [MICROORGA3]          VARCHAR (50)  NULL,
    [CUAL]                VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA352] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA352_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA352_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA352] NOCHECK CONSTRAINT [CK_HCFICHA352_JSON];


GO
ALTER TABLE [dbo].[HCFICHA352] NOCHECK CONSTRAINT [FK_HCFICHA352_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Version de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la prueba:  1=Cultivo   2=Hemocultivo   3=Biopsia   4=Radiografía   5=Tomografía axial computarizada   6=Renonancia nuclear magnética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODPRUEBA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la prueba:  1=Cultivo   2=Hemocultivo   3=Biopsia   4=Radiografía   5=Tomografía axial computarizada   6=Renonancia nuclear magnética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODPRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la prueba:  1=Cultivo   2=Hemocultivo   3=Biopsia   4=Radiografía   5=Tomografía axial computarizada   6=Renonancia nuclear magnética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODPRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la muestra  1=Sangre  2=Tejido  3=Otros líquidos estériles  4=Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODMUESTRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la muestra  1=Sangre  2=Tejido  3=Otros líquidos estériles  4=Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODMUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la muestra  1=Sangre  2=Tejido  3=Otros líquidos estériles  4=Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODMUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Laparoscopia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'LAPAROSCOPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo en que se le administró el antibiótico:   1=Antes   2=Durante   3=Después   4=Ninguna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'TIEMPOADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profilaxis:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROFILAXIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Orgaespac:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'ORGAESPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profusecun:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROFUSECUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profuprima:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROFUPRIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Supersecun:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'SUPERSECUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Superprima:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'SUPERPRIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Herida:   1=Limpia   2=Limpia contaminada   3=Herida contaminada   4=Herida sucia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'TIPOHERIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación ASA:   1=ASA 1   2=ASA 2   3=ASA 3   4=ASA 4   5=ASA 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CLASIFASA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anemia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'ANEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Preeclampsia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PREECLAMPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desnutricion:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obesidad:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'OBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Inmunosupre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diabetes:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'DIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Seleccione el procedimiento médico quirúrgico realizado:   1=Cesárea   2=Herniorrafía   3=Parto   4=Revascularización miocárdica con incisión torácica y del sitio donante   5=Colecistectomía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROCEDMEDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio de admisión del procedimiento quirúrgico o parto:   1=Programado ambulatorio   2=Urgencias   3=Programado hospitalizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'SERVIADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complejidad del servicio médico quirúrgico:   1=Baja   2=Media   3=Alta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'COMPLESERVI';

