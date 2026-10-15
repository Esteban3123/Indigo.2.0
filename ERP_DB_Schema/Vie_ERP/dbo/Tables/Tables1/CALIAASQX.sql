CREATE TABLE [dbo].[CALIAASQX] (
    [ID]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCALREPORTE]           INT           NOT NULL,
    [COMPLEJIDAD]            INT           NOT NULL,
    [SERVICIOPROCEDIMIENTO]  INT           NOT NULL,
    [PROCEDIMIENTOMEDICO]    INT           NOT NULL,
    [DIABETES]               BIT           NULL,
    [INMUNOSUPRESION]        BIT           NULL,
    [OBESIDAD]               BIT           NULL,
    [DESNUTRICION]           BIT           NULL,
    [PRECLAMPSIA]            BIT           NULL,
    [ANEMIA]                 BIT           NULL,
    [CLASIFICACIONASA]       INT           NOT NULL,
    [TIPOHERIDA]             INT           NOT NULL,
    [DURACIONPROCEDIMIENTO]  INT           NOT NULL,
    [SUPERFICIEPRIMARIA]     BIT           NULL,
    [SUPERFICIESECUNDAR]     BIT           NULL,
    [PROFUNDAPRIMARIA]       BIT           NULL,
    [PROFUNDASECUNDARIA]     BIT           NULL,
    [ORGANOESPACIO]          BIT           NULL,
    [PROFIAXISANTIBIOTICA]   BIT           NOT NULL,
    [CUAL]                   VARCHAR (200) NULL,
    [TIEMPOANTIBIOTICO]      INT           NOT NULL,
    [REQUIRIONUEVAINTERVEN]  BIT           NOT NULL,
    [FECHATOMAMUESTRA1]      DATE          NULL,
    [CODIGOMUESTRA1]         INT           NULL,
    [CODIGOPRUEBA1]          VARCHAR (3)   NULL,
    [MICROORGANISMOAISLADO1] VARCHAR (15)  NULL,
    [FECHATOMAMUESTRA2]      DATE          NULL,
    [CODIGOMUESTRA2]         INT           NULL,
    [CODIGOPRUEBA2]          VARCHAR (3)   NULL,
    [MICROORGANISMOAISLADO2] VARCHAR (15)  NULL,
    [FECHATOMAMUESTRA3]      DATE          NULL,
    [CODIGOMUESTRA3]         INT           NULL,
    [CODIGOPRUEBA3]          VARCHAR (3)   NULL,
    [MICROORGANISMOAISLADO3] VARCHAR (15)  NULL,
    CONSTRAINT [PK_CALIAASQX] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALIAASQX_CALREPORTE] FOREIGN KEY ([IDCALREPORTE]) REFERENCES [dbo].[CALREPORTE] ([ID])
);


GO
ALTER TABLE [dbo].[CALIAASQX] NOCHECK CONSTRAINT [FK_CALIAASQX_CALREPORTE];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Microorganismo aislado 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'MICROORGANISMOAISLADO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cód. de la prueba 3  55 = Cultivo92 = HemocultivoG3 = Biopsia86 = Radiografía90 = Tomografía axial computarizadaD4 = Renonancia nuclear magnética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOPRUEBA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cód. de la muestra 3  1 =  Sangre4 = Tejido11 = Otros Liquidos Esteriles32 = Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOMUESTRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Fecha de toma de la muestra 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Microorganismo aislado 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'MICROORGANISMOAISLADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cód. de la prueba 2  55 = Cultivo92 = HemocultivoG3 = Biopsia86 = Radiografía90 = Tomografía axial computarizadaD4 = Renonancia nuclear magnética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOPRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cód. de la muestra 2  1 =  Sangre4 = Tejido11 = Otros Liquidos Esteriles32 = Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOMUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Fecha de toma de la muestra 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Microorganismo aislado 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'MICROORGANISMOAISLADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cód. de la prueba 1  55 = Cultivo  92 = Hemocultivo  G3 = Biopsia  86 = Radiografía  90 = Tomografía axial computarizada  D4 = Renonancia nuclear magnética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOPRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cód. de la muestra 1  1 =  Sangre  4 = Tejido  11 = Otros Liquidos Esteriles  32 = Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CODIGOMUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Fecha de toma de la muestra 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Requirió una nueva intervención?  True = Si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'REQUIRIONUEVAINTERVEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo en que se le administró el antibiótico  1= Antes  2=Durante  3=Despues   4=Ninguna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'TIEMPOANTIBIOTICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profilaxis Antibiotica  Cual?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profilaxis antibiótica  Profilaxis antibiótica relacionada con el Proc.Qx o parto    1 = True = Si  0 = False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROFIAXISANTIBIOTICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de infección sitio quirúrgico  1 = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'ORGANOESPACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de infección sitio quirúrgico  1 = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROFUNDASECUNDARIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de infección sitio quirúrgico  1 = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROFUNDAPRIMARIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de infección sitio quirúrgico  1 = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'SUPERFICIESECUNDAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de infección sitio quirúrgico  1 = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'SUPERFICIEPRIMARIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duración del Procedimiento (Minutos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'DURACIONPROCEDIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de herida  1=Limpia  2=Limpia contaminada  3=Herida contaminada   4=Herida sucia ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'TIPOHERIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indice de riesgo NHSN-NNISS  Clasificación ASA    1=ASA 1  2=ASA 2  3=ASA 3  4=ASA 4  5=ASA 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONASA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factores de riesgo  1 = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'ANEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factores de riesgo  1 = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PRECLAMPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factores de riesgo  1 = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factores de riesgo  1 = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'OBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factores de riesgo  1 = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factores de riesgo  1 = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'DIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Seleccione el procedimiento médico quirúrgico realizado  1=Cesárea  2=Herniorrafia  3=Parto  4=Revascularización miocardica con incisión torácica y del sitio donante  5=Colecistectomía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'PROCEDIMIENTOMEDICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio de admisión del  procedimiento qx o parto  1 = Programado ambulatorio  2 = Urgencias  3 = Programado hospitalizado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'SERVICIOPROCEDIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complejidad del servicio médico quirurgico  1= Baja   2= Media  3= Alta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'COMPLEJIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del reporte que guarda igual que id del tabla CALREPORTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASQX', @level2type = N'COLUMN', @level2name = N'ID';

