CREATE TABLE [dbo].[INPLANBEN] (
    [CODPLABEN]  VARCHAR (10)  NOT NULL,
    [DESPLABEN]  VARCHAR (120) NOT NULL,
    [OBTCONTRA]  INT           NULL,
    [LIQPLABEN]  CHAR (1)      NOT NULL,
    [CANTUSUAR]  INT           NULL,
    [VALORUSUA]  MONEY         NULL,
    [PLAPERFAC]  CHAR (1)      NULL,
    [PLATOPIND]  MONEY         NULL,
    [PLATOPPER]  MONEY         NULL,
    [PLANREQUE]  INT           NULL,
    [CXCENTIDA]  INT           NULL,
    [PLAAFECAR]  CHAR (1)      NULL,
    [NUMCONEXE]  INT           NULL,
    [HORMINEXT]  INT           NULL,
    [PORDESCON]  MONEY         NULL,
    [PORDESURG]  MONEY         NULL,
    [CUOTMODER]  INT           NULL,
    [CUOTCOPAG]  INT           NULL,
    [DIAPLAFACT] INT           NULL,
    [PLAPORSER]  INT           NULL,
    [PLAPROMED]  INT           NULL,
    [PLACENCOS]  INT           NULL,
    [PLAPOSSUB]  BIT           NULL,
    [PLAAUTING]  BIT           NULL,
    [PLALIQPFGP] BIT           NULL,
    [MANCONEXT]  BIT           NULL,
    [TERLIQISS]  BIT           NULL,
    [ACTPREVEN]  BIT           NULL,
    [CXCPACIENT] INT           NULL,
    [CONRECAJA]  INT           NULL,
    [CONANTICI]  INT           NULL,
    [CODTERCER]  INT           NULL,
    [PLAPRPFGP]  INT           NULL,
    [PLATIPENT]  CHAR (1)      NULL,
    [PLAPROACC]  INT           NULL,
    [PORACTMED]  MONEY         NULL,
    [PLAPROMPP]  INT           NULL,
    [PORACTSUM]  MONEY         NULL,
    [LIQCIRART]  BIT           NULL,
    [CONCEPFAC]  CHAR (2)      NULL,
    [PLANINACT]  BIT           NULL,
    [CODMERCAD]  INT           NULL,
    [TIPPACPFGP] CHAR (1)      NULL,
    [PLAVENDED]  INT           NULL,
    [PLATIPLIQ]  CHAR (1)      NULL,
    [CARPERGLO]  VARCHAR (120) NULL,
    [NOMPERGLO]  VARCHAR (120) NULL,
    [CONDVENT]   INT           NULL,
    [CODASEGUR]  VARCHAR (120) NULL,
    [VIGPREFAC]  INT           NULL,
    [RUBROFAC]   INT           NULL,
    [TIPINGFAC]  INT           NULL,
    [VIGPREPAG]  INT           NULL,
    [RUBROPAG]   INT           NULL,
    [TIPINGPAG]  INT           NULL,
    [CUENTOBJE]  INT           NULL,
    [INTFAUTOR]  BIT           NULL,
    [DEPFACTUR]  INT           NULL,
    [DEPPAGARE]  INT           NULL,
    CONSTRAINT [PK_INPLANBEN_1] PRIMARY KEY CLUSTERED ([CODPLABEN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dependencia Asociada al Pagare', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DEPPAGARE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dependencia Asociada a la Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DEPFACTUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Interfaz Autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'INTFAUTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Objecion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CUENTOBJE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Ingresos Pagare', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TIPINGPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rubro Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'RUBROPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vigencia Presupuestal Pagare', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'VIGPREPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Ingresos Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TIPINGFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rubro Ingresos Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'RUBROFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vigencia Presupuestal de la Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'VIGPREFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Aseguradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODASEGUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Condiciones de Venta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONDVENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Persona que Glosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'NOMPERGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargo de la Persona que Recibe la Glosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CARPERGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Liquidacion plan de beneficios  1:ISS;  2:SOAT;  3:ISS 2004;  4:ISS 2001', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATIPLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vendedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAVENDED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paciente PFGP   1= Vinculado  2=Desplazado  3=PyP Vinculado   4=PyP Desplazado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TIPPACPFGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Mercado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODMERCAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desactivar el Plan ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLANINACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto a Facturar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONCEPFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquidacion de cirugias Artroscopicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'LIQCIRART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Suministros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORACTSUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantillas Procedimientos de Metas PYP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPROMPP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Actualizacion Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantillas Procedimientos para tercera factura de accidentes de transito ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPROACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Entidad  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATIPENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantilla Procedimientos PFGP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPRPFGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODTERCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto Anticipo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONANTICI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto Recibo de Caja', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CONRECAJA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta por Cobrar al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CXCPACIENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Actualizar Precios de Venta ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'ACTPREVEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercera Factura Accidentes de Transito Liquida ISS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'TERLIQISS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo de Consultas Exentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'MANCONEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquidar PFGP 1=Si;0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLALIQPFGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pedir Autorizacion En ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAAUTING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POS Subsidiado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPOSSUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centros de Costo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLACENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantillas Procedimientos Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPROMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantilla Procedimientos Servios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPORSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Planzo en Dias de la Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DIAPLAFACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuota por Cobrar a Copago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CUOTCOPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta por Cobrar Cuota Moderadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CUOTMODER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '% Descuento Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORDESURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '% Descuento de Medicamentos en Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PORDESCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas Minimas Para Liquidar la Extancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'HORMINEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Consultas Excentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'NUMCONEXE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta Cartera  1:Por Factura  2:Cuenta Cobro  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAAFECAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta por Cobrar a la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CXCENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantilla Requeriminetos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLANREQUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tope por Periodo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATOPPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Topo Individual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLATOPIND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodo Facturacion  1:Semanal;  2:Quincenal;  3:Mensual;  4:Bimensual;  5:Semestral;  6:Anual  6:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'PLAPERFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Usuarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'VALORUSUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Usuarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CANTUSUAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquidacion  1:Pago por Servicios  2:Capitacion  3:Factura Global   4:Capitacion Global  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'LIQPLABEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'OBTCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Plan Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'DESPLABEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANBEN', @level2type = N'COLUMN', @level2name = N'CODPLABEN';

