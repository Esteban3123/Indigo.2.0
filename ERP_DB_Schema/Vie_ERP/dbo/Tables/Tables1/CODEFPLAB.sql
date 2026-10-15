CREATE TABLE [dbo].[CODEFPLAB] (
    [CODCONTRA]         CHAR (6)        NOT NULL,
    [CODPLANBE]         CHAR (2)        NOT NULL,
    [CODENTADM]         CHAR (9)        NOT NULL,
    [NOMPLANBE]         VARCHAR (120)   NULL,
    [OBTCONTRA]         INT             NULL,
    [TIPLIQUID]         INT             NOT NULL,
    [CANTUSUAR]         INT             NULL,
    [VALORUSUA]         MONEY           NULL,
    [PERIODLIQ]         INT             NOT NULL,
    [TOPEINDVI]         NUMERIC (18, 2) NOT NULL,
    [TOPEPERIO]         NUMERIC (18, 2) NOT NULL,
    [AFECTACAR]         INT             NOT NULL,
    [CCSERVICI]         CHAR (15)       NOT NULL,
    [CCCOPAGOS]         CHAR (15)       NOT NULL,
    [CCCTAMODE]         CHAR (15)       NOT NULL,
    [CCCXCPACI]         CHAR (15)       NOT NULL,
    [CODCENCOS]         CHAR (14)       NOT NULL,
    [PLAZOFACT]         INT             NOT NULL,
    [GPPCODIGZ]         CHAR (3)        NOT NULL,
    [GPICODIGZ]         CHAR (3)        NOT NULL,
    [CODPLANRE]         CHAR (3)        NOT NULL,
    [CODPLAHOS]         CHAR (3)        NULL,
    [CONRECPAC]         CHAR (3)        NULL,
    [CONRECENT]         CHAR (3)        NULL,
    [ESTADOPLA]         INT             NOT NULL,
    [NUMCONEXE]         INT             NULL,
    [HORMINEXT]         INT             NULL,
    [PORDESCON]         MONEY           NULL,
    [PORDESURG]         MONEY           NULL,
    [PLAPOSSUB]         BIT             NULL,
    [PLAAUTING]         BIT             NULL,
    [PLALIQPFGP]        BIT             NULL,
    [MANCONEXT]         CHAR (1)        NULL,
    [TERLIQISS]         BIT             NULL,
    [ACTPREVEN]         BIT             NULL,
    [CODTERCER]         CHAR (15)       NULL,
    [PLAPRPFGP]         CHAR (3)        NULL,
    [PLATIPENT]         CHAR (2)        NULL,
    [PLAPROACC]         INT             NULL,
    [PORACTMED]         MONEY           NULL,
    [PLAPROMPP]         INT             NULL,
    [PORACTSUM]         MONEY           NULL,
    [LIQCIRART]         BIT             NULL,
    [CONCEPFAC]         CHAR (4)        NULL,
    [CODMERCAD]         CHAR (2)        NULL,
    [TIPPACPFGP]        CHAR (1)        NULL,
    [PLAVENDED]         VARCHAR (20)    NULL,
    [PLATIPLIQ]         CHAR (1)        NULL,
    [CARPERGLO]         VARCHAR (120)   NULL,
    [NOMPERGLO]         VARCHAR (120)   NULL,
    [CONDVENT]          INT             NULL,
    [CODASEGUR]         VARCHAR (120)   NULL,
    [VIGPREFAC]         INT             NULL,
    [RUBROFAC]          INT             NULL,
    [TIPINGFAC]         INT             NULL,
    [VIGPREPAG]         INT             NULL,
    [RUBROPAG]          INT             NULL,
    [TIPINGPAG]         INT             NULL,
    [CUENTOBJE]         CHAR (15)       NULL,
    [DEPFACTUR]         INT             NULL,
    [INTFAUTOR]         BIT             NULL,
    [DEPPAGARE]         INT             NULL,
    [MONFACTDE]         CHAR (1)        NULL,
    [LIQCOPCTA]         CHAR (1)        NULL,
    [CCSERVICR]         CHAR (15)       NULL,
    [INDAUDFOR]         NUMERIC (18, 9) NOT NULL,
    [CCXPERCAP]         CHAR (15)       NULL,
    [TIPOREGIM]         CHAR (1)        NULL,
    [CCNTRAMITE]        CHAR (16)       NULL,
    [CCCOJURIDICO]      CHAR (16)       NULL,
    [CCRADCONCIL]       CHAR (16)       NULL,
    [DEPMUNCOD]         CHAR (5)        NULL,
    [NOEXIGIRJUSTNOPOS] BIT             NULL,
    [ENTETERRITORIAL]   BIT             NULL,
    CONSTRAINT [PK_INPLANBEN] PRIMARY KEY CLUSTERED ([CODCONTRA] ASC, [CODPLANBE] ASC),
    CONSTRAINT [FK_CODEFPLAB_INMUNICIP] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD])
);


GO
ALTER TABLE [dbo].[CODEFPLAB] NOCHECK CONSTRAINT [FK_CODEFPLAB_INMUNICIP];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si es o no ente territorial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'ENTETERRITORIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si exige o no justificacion no pos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NOEXIGIRJUSTNOPOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de municipios ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable por Radicada Conciliada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCRADCONCIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable por Cobro Jurídico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCOJURIDICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable por Cuenta trámite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCNTRAMITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Regimen  1- contributivo  2 - Subsidiado  3 - Excepcion  4 - especial  5 - No asegurado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPOREGIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta por Cobrar Perd. Capitacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCXPERCAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoria INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Contable para Servicios IPS CxC Radicados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCSERVICR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquidacion de Copago/ Cuota  1:Estandar  2:Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'LIQCOPCTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tomar Monto Facturado de:  1:Total Servicio   2:Valor Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'MONFACTDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dependencia Asociada al Pagare', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'DEPPAGARE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Interfaz Autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'INTFAUTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dependencia Asociada a la Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'DEPFACTUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Objecion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CUENTOBJE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Ingresos Pagare', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPINGPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rubro Pago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'RUBROPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vigencia Presupuestal Pagare', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'VIGPREPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Ingresos Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPINGFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rubro Ingresos Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'RUBROFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vigencia Presupuestal de la Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'VIGPREFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Aseguradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODASEGUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Condiciones de Venta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONDVENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Persona que Glosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NOMPERGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargo de la Persona que Recibe la Glosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CARPERGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Liquidacion plan de beneficios  1:ISS 2001;  2:SOAT;  3:ISS 2004;  4:CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLATIPLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vendedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAVENDED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Paciente PFGP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPPACPFGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Mercado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODMERCAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto a Facturar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONCEPFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquidacion de cirugias Artroscopicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'LIQCIRART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Actualizacion de Suministros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORACTSUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantillas Procedimientos de Metas PYP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPROMPP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Actualizacion Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantillas Procedimientos para tercera factura de accidentes de transito ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPROACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Entidad  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLATIPENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantilla Procedimientos PFGP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPRPFGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODTERCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Actualizar Precios de Venta ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'ACTPREVEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercera Factura Accidentes de Transito Liquida ISS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TERLIQISS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo de Consultas Exentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'MANCONEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantilla PFGP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLALIQPFGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pedir Autorizacion En ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAAUTING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POS Subsidiado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAPOSSUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '% Descuento Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORDESURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '% Descuento de Medicamentos en Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PORDESCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas Minimas Para Liquidar la Extancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'HORMINEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Consultas Excentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NUMCONEXE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Plan de Beneficios:  0: Bloqueado No    1: Bloqueado Si  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'ESTADOPLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Concepto del Recibo de Caja para la Cancelacion de las Cuotas de Recuperacion de las Facturas a Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONRECENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Concepto del Recibo de Caja para la Cancelacion de Facturas a Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CONRECPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantilla de Hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODPLAHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantilla de Requerimientos para la atencion de pacientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODPLANRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantilla de Medicamentos - Trae el valor de los medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'GPICODIGZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantilla para Servicios IPS - Trae el valor de los servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'GPPCODIGZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plazo para el vencimiento de facturas o Cuentas de Cobro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PLAZOFACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Costos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODCENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Contable para las Cuenas por Cobrar a Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCXCPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Contable para el recaudo de Cuotas moderadoras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCTAMODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Contable para el recaudo de Copagos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCCOPAGOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Contable para Servicios IPS CxC sin Radicar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CCSERVICI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Forma como se Afecta la Cartera:  1=Por Factura    2=Cuenta de Cobro  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'AFECTACAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tope Maximo para la liquidacion en el periodo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TOPEPERIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tope maximo para la liquidacion individual o Factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TOPEINDVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodo de Liquidacion para el control de Topes:  1=Semanal    2=Quincenal    3=Mensual    4=Bimensual    5=Semestral    6=Anual  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'PERIODLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Usuarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'VALORUSUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Usuarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CANTUSUAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Liquidacion del Contrato / PB:  1: Pago por Evento - Servicios  2: Capitacion  3:Factura Global   4:Capitacion Global  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'TIPLIQUID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Objeto del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'OBTCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del plan de beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'NOMPLANBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Entidad Administradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODENTADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODPLANBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CODEFPLAB', @level2type = N'COLUMN', @level2name = N'CODCONTRA';

