CREATE TABLE [dbo].[HCFICHA880] (
    [ID]                  INT            IDENTITY (1, 1) NOT NULL,
    [IDFICHANOTIFICACION] INT            NOT NULL,
    [ERUPCION]            INT            NULL,
    [TIPOERUPCION]        INT            NULL,
    [FECHAINIERUPCION]    DATE           NULL,
    [FIEBRE]              INT            NULL,
    [ULCEGENITALES]       INT            NULL,
    [OTROSINTOMAS]        INT            NULL,
    [CUALESOTROSINTOMAS]  VARCHAR (1000) NULL,
    [COMPLICACIONES]      INT            NULL,
    [TUVOCONTPROBA]       INT            NULL,
    [FECHACONTACTO]       DATE           NULL,
    [TUVOCONTSEXUAL]      INT            NULL,
    [TIENENUEVAPSEXUAL]   INT            NULL,
    [ANTEVIAJEEXT21D]     INT            NULL,
    [CUALPAISEXTVIAJO]    VARCHAR (100)  NULL,
    [SEGUICONTACTOS]      INT            NULL,
    [FECHAFINALSEGCONT]   DATE           NULL,
    [FUENTEINFECCION]     INT            NULL,
    [CASODESCARTADO]      INT            NULL,
    [COMPLICEREBRAL]      BIT            NULL,
    [COMPLIPULMONAR]      BIT            NULL,
    [COMPLIOFTALMICA]     BIT            NULL,
    [FECHATOMA1]          DATE           NULL,
    [FECHARECEP1]         DATE           NULL,
    [MUESTRA1]            INT            NULL,
    [PRUEBA1]             INT            NULL,
    [AGENTE1]             INT            NULL,
    [RESULTADO1]          INT            NULL,
    [FECHARES1]           DATE           NULL,
    [VALOREGIS1]          VARCHAR (20)   NULL,
    [FECHATOMA2]          DATE           NULL,
    [FECHARECEP2]         DATE           NULL,
    [MUESTRA2]            INT            NULL,
    [PRUEBA2]             INT            NULL,
    [AGENTE2]             INT            NULL,
    [RESULTADO2]          INT            NULL,
    [FECHARES2]           DATE           NULL,
    [VALOREGIS2]          VARCHAR (20)   NULL,
    [FECHATOMA3]          DATE           NULL,
    [FECHARECEP3]         DATE           NULL,
    [MUESTRA3]            INT            NULL,
    [PRUEBA3]             INT            NULL,
    [AGENTE3]             INT            NULL,
    [RESULTADO3]          INT            NULL,
    [FECHARES3]           DATE           NULL,
    [VALOREGIS3]          VARCHAR (20)   NULL,
    [FECHATOMA4]          DATE           NULL,
    [FECHARECEP4]         DATE           NULL,
    [MUESTRA4]            INT            NULL,
    [PRUEBA4]             INT            NULL,
    [AGENTE4]             INT            NULL,
    [RESULTADO4]          INT            NULL,
    [FECHARES4]           DATE           NULL,
    [VALOREGIS4]          VARCHAR (20)   NULL,
    [VERSION]             VARCHAR (20)   NULL,
    [JSON]                VARCHAR (MAX)  NULL,
    [CODDIAGNO]           NCHAR (4)      NULL,
    CONSTRAINT [PK_HCFICHA880] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA880_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA880_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA880] NOCHECK CONSTRAINT [CK_HCFICHA880_JSON];


GO
ALTER TABLE [dbo].[HCFICHA880] NOCHECK CONSTRAINT [FK_HCFICHA880_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PAIS_TEXTO = texto (Nombre Pais internacional viaje)    PAIS_CODIGO = texto (Codigo Pais internacional viaje, 3 digitos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del resultado 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado:   1= Positivo   2= Negativo   3= No procesado   4= Inadecuado   5= Dudoso  6= Valor registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente:   1=J Monkeypox   2= Otro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba:  1=PCR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestra: 1= Hisopado nasofaríngeo   2= Tejido  3= Lcr   4= Suero   5=Orofaríngeo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del resultado 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado:   1= Positivo   2= Negativo   3= No procesado   4= Inadecuado   5= Dudoso  6= Valor registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente:   1=J Monkeypox   2= Otro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba:   1=PCR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestra:   1= Hisopado nasofaríngeo   2= Tejido  3= Lcr   4= Suero  5=Orofaríngeo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del resultado 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado:   1= Positivo   2= Negativo   3= No procesado   4= Inadecuado   5= Dudoso  6= Valor registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente:   1=J Monkeypox   2= Otro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba:   1=PCR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestra:   1= Hisopado nasofaríngeo   2= Tejido  3= Lcr   4= Suero  5=Orofaríngeo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'VALOREGIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del resultado 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARES1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado:   1= Positivo   2= Negativo   3= No procesado   4= Inadecuado   5= Dudoso  6= Valor registrado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'RESULTADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente:   1=J Monkeypox   2= Otro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'AGENTE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba:   1=PCR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'PRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestra:   1= Hisopado nasofaríngeo   2= Tejido  3= Lcr   4= Suero  5=Orofaríngeo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'MUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHARECEP1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMPLIOFTALMICA:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLIOFTALMICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMPLIPULMONAR:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLIPULMONAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMPLICEREBRAL:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLICEREBRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si el caso es descartado, señale el criterio para descartar:   1=Laboratorio negativo  2=Dengue  3=Herpes 6,6  4=Reacción alérgica  5=Varicela  6=Sífilis  7=Otro diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CASODESCARTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si el caso fue confirmado, señale fuente de infección:   1=Importado  2=Relacionado con la importación  3=Fuente desconocida  4=Relacionado con desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FUENTEINFECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final de seguimiento a contactos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHAFINALSEGCONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' ¿Se realizó seguimiento a contactos? :  1=Si  2=No  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'SEGUICONTACTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Donde: (lugar internacional)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CUALPAISEXTVIAJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Antecedentes de viaje al exterior en los últimos 21 días :  1=Si  2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ANTEVIAJEEXT21D';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' ¿Tiene nueva o múltiples parejas sexuales? :  1=Si  2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TIENENUEVAPSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' ¿Tuvo contacto estrecho (incluso sexual) con personal procedente del extranjero? :  1=Si  2=No  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TUVOCONTSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHACONTACTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Tuvo contacto con caso probable/Confirmado de Monkeypox en los últimos 21 días? :  1=Si  2=No  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TUVOCONTPROBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones:  1=Si   2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'COMPLICACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CUALES OTROS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'CUALESOTROSINTOMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otros signos/síntomas:  1=Si   2=No  3=Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'OTROSINTOMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Úlceras genitales:   1=Si  2=No  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ULCEGENITALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fiebre:   1=Si   2=No  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Inicio de erupción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'FECHAINIERUPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Erupcion:   1=Vesicular   2=Máculopapular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'TIPOERUPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ERUPCION:   1=Si   2=No  3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ERUPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  el Id de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA880', @level2type = N'COLUMN', @level2name = N'ID';

