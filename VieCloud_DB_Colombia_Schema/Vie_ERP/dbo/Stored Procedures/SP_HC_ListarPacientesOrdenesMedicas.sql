
-- =============================================
-- Author:		Juan Patiño 
-- Create date: 29/02/2016
-- Description:	SP que lista los pacientes Que Tiene Ordenes Medicas.
-- =============================================

CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesOrdenesMedicas]
(
@CentroAtencion Char(10) ,
@UFuncional char(20)  
)
AS
BEGIN
	SET NOCOUNT ON;

	   CREATE TABLE #Pacientes(
        [ID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
        [CodigoUnidadFuncional] VARCHAR(20) NULL,
        [CodigoTipoServicio] VARCHAR(10) NULL,
        [NombreTipoServicio] VARCHAR(100) NULL,
        [Identificacion] VARCHAR(36) NULL,
        [Ingreso] VARCHAR(36) NULL,
        [FechaSolicitud] DATETIME NULL,
        [CodigoServicio] VARCHAR(50) NULL,
        [NombreServicio] VARCHAR(500) NULL,
        [EstadoServicio] VARCHAR(10) NULL,
        [NombreEstado] VARCHAR(50) NULL,
        [CodigoEstado] INT,
        [NumeroFolio] INT,
        [CodigoEstadoServicio] INT,
        [Autorizacion] VARCHAR(50) NULL,
        [EntityId] INT NOT NULL,
        [Cantidad] INT,
        [Cantidadrea] INT,
        [Terapia] INT,
        [CodigoMotivoMuestraNoConforme] VARCHAR(50) NULL,
        [DescripcionMotivoMuestraNoConforme] VARCHAR(200) NULL,
        [ServicioPBS] VARCHAR(20) NULL
    );

    -- Índices en tabla temporal para mejorar JOINs
    CREATE NONCLUSTERED INDEX IX_Pacientes_Ingreso ON #Pacientes(Ingreso);
    CREATE NONCLUSTERED INDEX IX_Pacientes_Identificacion ON #Pacientes(Identificacion);
    CREATE NONCLUSTERED INDEX IX_Pacientes_CodigoUnidadFuncional ON #Pacientes(CodigoUnidadFuncional);

    -- =============================================
    -- TABLA DE INGRESOS FILTRADOS (Con índice)
    -- =============================================
    DECLARE @TablaIngresosFiltro AS TABLE(
        Ingreso VARCHAR(36) PRIMARY KEY
    );

    INSERT INTO @TablaIngresosFiltro 
    SELECT DISTINCT C.NUMINGRES
    FROM dbo.CHCAMASHO A 
    LEFT JOIN dbo.CHREGESTA C ON A.CODICAMAS = C.CODICAMAS AND C.REGESTADO = 1 
    LEFT JOIN dbo.ADINGRESO I ON I.NUMINGRES = C.NUMINGRES 
    WHERE A.CODCENATE = @CentroAtencion 
        AND A.UFUCODIGO = @UFuncional 
        AND ESTADCAMA = '2' 
        AND IESTADOIN IN ('', 'P')
    
    UNION ALL
    
    SELECT A.NUMINGRES
    FROM dbo.ADINGRESO A
    WHERE IESTADOIN IN ('', 'P')  
        AND A.CODCENATE = @CentroAtencion 
        AND UFUINGMED = @UFuncional 
        AND UFUINGHOS IS NULL 
        AND (UFUEGRMED IS NULL OR DESTINOPAC = 1) 
        AND DESTINOPAC IS NOT NULL;

    --IF NOT EXISTS (SELECT 1 FROM @TablaIngresosFiltro) 
    --    RETURN;

    -- =============================================
    -- TABLAS AUXILIARES PARA EVITAR SUBCONSULTAS CORRELACIONADAS
    -- =============================================
    
    -- Conteo de terapias realizadas
    DECLARE @TerapiasRealizadas AS TABLE(
        IPCODPACI VARCHAR(36),
        NUMINGRES VARCHAR(36),
        CODSERIPS VARCHAR(50),
        CantidadRealizadas INT,
        PRIMARY KEY (IPCODPACI, NUMINGRES, CODSERIPS)
    );

    INSERT INTO @TerapiasRealizadas
    SELECT 
        IPCODPACI,
        NUMINGRES,
        CODSERIPS,
        COUNT(*) AS CantidadRealizadas
    FROM HCPROCTER
    WHERE NUMINGRES IN (SELECT Ingreso FROM @TablaIngresosFiltro)
    GROUP BY IPCODPACI, NUMINGRES, CODSERIPS;

    -- =============================================
    -- INSERTAR DATOS EN TABLA TEMPORAL (6 UNION ALL)
    -- =============================================
    INSERT INTO #Pacientes(
        [CodigoUnidadFuncional],[CodigoTipoServicio],[NombreTipoServicio],
        [Identificacion],[Ingreso],[FechaSolicitud],[CodigoServicio],
        [NombreServicio],[EstadoServicio],[NombreEstado],[CodigoEstado],
        [NumeroFolio],[CodigoEstadoServicio],[Autorizacion],[Cantidad],
        [Cantidadrea],[EntityId],[Terapia],[CodigoMotivoMuestraNoConforme],
        [DescripcionMotivoMuestraNoConforme],[ServicioPBS]
    )
    -- =============================================
    -- 1. IMAGENES DIAGNOSTICAS
    -- =============================================
    SELECT DISTINCT 
        A.UFUCODIGO,
        B.TIPSERIPS,
        'Imagenes Diagnosticas',
        A.IPCODPACI,
        A.NUMINGRES,
        A.FECORDMED,
        RTRIM(B.CODSERIPS),
        RTRIM(B.DESSERIPS),
        A.ESTSERIPS, 
        CASE A.ESTSERIPS 
            WHEN 1 THEN 'Solicitado' 
            WHEN 2 THEN 'Realizando Estudio' 
            WHEN 3 THEN 'Imagen Procesada' 
            WHEN 5 THEN 'Estudio Remitido' 
        END,
        B.TIPSERIPS,
        A.NUMEFOLIO,
        A.ESTSERIPS,
        CASE 
            WHEN (S.SUSCEPTIB IS NULL OR S.SUSCEPTIB = 0 OR SS.PROESTADO IS NULL) THEN 'No requiere autorización' 
            WHEN SS.PROESTADO = 1 THEN 'Pendiente Solicitud'
            WHEN SS.PROESTADO = 2 THEN 'Solicitado'
            WHEN SS.PROESTADO = 3 THEN 'Autorizado'
            WHEN SS.PROESTADO = 4 THEN 'Anulado'
            WHEN SS.PROESTADO = 5 THEN 'No autorizado'
            ELSE 'Pendiente por Autorizar'
        END,
        A.CANSERIPS,
        0,
        A.AUTO,
        0,
        '',
        '',
        CASE B.SERIPSPOS WHEN 1 THEN 'Si' ELSE 'NO' END
    FROM HCORDIMAG A
    INNER JOIN INCUPSIPS B ON A.CODSERIPS = B.CODSERIPS 
    INNER JOIN @TablaIngresosFiltro TIF ON A.NUMINGRES = TIF.Ingreso
    LEFT JOIN ADCONFSER S ON S.CODSERIPS = A.CODSERIPS	
    LEFT JOIN ADAUTOSER SS ON SS.IPCODPACI = A.IPCODPACI 
        AND SS.NUMINGRES = A.NUMINGRES 
        AND SS.CODSERIPS = A.CODSERIPS 
        AND SS.NUMEFOLIO = A.NUMEFOLIO
    WHERE A.CODCENATE = @CentroAtencion  
        AND A.ESTSERIPS IN (1,2,3,5)
    
    UNION ALL
    
    -- =============================================
    -- 2. LABORATORIO
    -- =============================================
    SELECT DISTINCT  
        C.UFUCODIGO,
        D.TIPSERIPS,
        'Laboratorio',
        C.IPCODPACI,
        C.NUMINGRES,
        C.FECORDMED,
        RTRIM(D.CODSERIPS),
        RTRIM(D.DESSERIPS),
        C.ESTSERIPS,
        CASE C.ESTSERIPS 
            WHEN 1 THEN 'Solicitado' 
            WHEN 2 THEN 'Muestra recolectada' 
            WHEN 3 THEN 'Resultado Entregado' 
            WHEN 5 THEN 'Estudio remitido' 
            WHEN 9 THEN 'Muestra no conforme' 
        END,
        D.TIPSERIPS,
        C.NUMEFOLIO,
        C.ESTSERIPS, 
        CASE 
            WHEN (S.SUSCEPTIB IS NULL OR S.SUSCEPTIB = 0 OR SS.PROESTADO IS NULL) THEN 'No requiere autorización' 
            WHEN SS.PROESTADO = 1 THEN 'Pendiente Solicitud'
            WHEN SS.PROESTADO = 2 THEN 'Solicitado'
            WHEN SS.PROESTADO = 3 THEN 'Autorizado'
            WHEN SS.PROESTADO = 4 THEN 'Anulado'
            WHEN SS.PROESTADO = 5 THEN 'No autorizado'
            ELSE 'Pendiente por Autorizar'
        END,
        C.CANSERIPS,
        0,
        C.AUTO,
        0,
        ISNULL(C.CODMOTIVOMUESTRANOCONFORME,''),
        ISNULL(RTRIM(MA.DESMOTANU),''),
        CASE D.SERIPSPOS WHEN 1 THEN 'Si' ELSE 'NO' END
    FROM HCORDLABO C
    INNER JOIN INCUPSIPS D ON C.CODSERIPS = D.CODSERIPS 
    INNER JOIN @TablaIngresosFiltro TIF ON C.NUMINGRES = TIF.Ingreso
    LEFT JOIN ADCONFSER S ON S.CODSERIPS = C.CODSERIPS 
    LEFT JOIN ADAUTOSER SS ON SS.IPCODPACI = C.IPCODPACI 
        AND SS.NUMINGRES = C.NUMINGRES 
        AND SS.CODSERIPS = C.CODSERIPS 
        AND SS.NUMEFOLIO = C.NUMEFOLIO
    LEFT JOIN HCMOANULB MA ON MA.CODMOTANU = C.CODMOTIVOMUESTRANOCONFORME
    WHERE C.CODCENATE = @CentroAtencion 
        AND C.ESTSERIPS IN (1,2,3,5,9)
    
    UNION ALL
    
    -- =============================================
    -- 3. INTERCONSULTAS
    -- =============================================
    SELECT DISTINCT 
        E.UFUCODIGO,
        F.TIPSERIPS,
        'Interconsultas',
        E.IPCODPACI,
        E.NUMINGRES,
        E.FECORDMED,
        RTRIM(F.CODSERIPS),
        RTRIM(F.DESSERIPS),
        E.ESTSERIPS, 
        CASE E.ESTSERIPS 
            WHEN 1 THEN 'Solicitado' 
            WHEN 2 THEN 'Solicitud Enviada'  
        END,
        F.TIPSERIPS,
        E.NUMEFOLIO,
        E.ESTSERIPS,
        CASE 
            WHEN (S.SUSCEPTIB IS NULL OR S.SUSCEPTIB = 0 OR SS.PROESTADO IS NULL) THEN 'No requiere autorización' 
            WHEN SS.PROESTADO = 1 THEN 'Pendiente Solicitud'
            WHEN SS.PROESTADO = 2 THEN 'Solicitado'
            WHEN SS.PROESTADO = 3 THEN 'Autorizado'
            WHEN SS.PROESTADO = 4 THEN 'Anulado'
            WHEN SS.PROESTADO = 5 THEN 'No autorizado'
            ELSE 'Pendiente por Autorizar'
        END,
        E.CANSERIPS,
        0,
        E.AUTO,
        0,
        '',
        '',
        CASE F.SERIPSPOS WHEN 1 THEN 'Si' ELSE 'NO' END
    FROM HCORDINTE E
    INNER JOIN INCUPSIPS F ON E.CODSERIPS = F.CODSERIPS 
    INNER JOIN @TablaIngresosFiltro TIF ON E.NUMINGRES = TIF.Ingreso
    LEFT JOIN ADCONFSER S ON S.CODSERIPS = E.CODSERIPS  
    LEFT JOIN ADAUTOSER SS ON SS.IPCODPACI = E.IPCODPACI 
        AND SS.NUMINGRES = E.NUMINGRES 
        AND SS.CODSERIPS = E.CODSERIPS 
        AND SS.NUMEFOLIO = E.NUMEFOLIO
    WHERE E.CODCENATE = @CentroAtencion  
        AND E.ESTSERIPS IN (1,2)
    
    UNION ALL
    
    -- =============================================
    -- 4. PATOLOGIAS
    -- =============================================
    SELECT  
        G.UFUCODIGO,
        H.TIPSERIPS,
        'Patologias',
        G.IPCODPACI,
        G.NUMINGRES,
        G.FECORDMED,
        RTRIM(H.CODSERIPS),
        RTRIM(H.DESSERIPS),
        G.ESTSERIPS, 
        CASE G.ESTSERIPS 
            WHEN 1 THEN 'Solicitado' 
            WHEN 2 THEN 'Muestra Recolectada' 
            WHEN 3 THEN 'Resultado del Estudio' 
            WHEN 5 THEN 'Estudio Remitido'  
        END,
        H.TIPSERIPS,
        G.NUMEFOLIO,
        G.ESTSERIPS,
        CASE 
            WHEN (S.SUSCEPTIB IS NULL OR S.SUSCEPTIB = 0 OR SS.PROESTADO IS NULL) THEN 'No requiere autorización' 
            WHEN SS.PROESTADO = 1 THEN 'Pendiente Solicitud'
            WHEN SS.PROESTADO = 2 THEN 'Solicitado'
            WHEN SS.PROESTADO = 3 THEN 'Autorizado'
            WHEN SS.PROESTADO = 4 THEN 'Anulado'
            WHEN SS.PROESTADO = 5 THEN 'No autorizado'
            ELSE 'Pendiente por Autorizar'
        END,
        G.CANSERIPS,
        0,
        G.AUTO,
        0,
        '',
        '',
        CASE H.SERIPSPOS WHEN 1 THEN 'Si' ELSE 'NO' END
    FROM HCORDPATO G
    INNER JOIN INCUPSIPS H ON H.CODSERIPS = G.CODSERIPS 
    INNER JOIN @TablaIngresosFiltro TIF ON G.NUMINGRES = TIF.Ingreso
    LEFT JOIN ADCONFSER S ON S.CODSERIPS = G.CODSERIPS  
    LEFT JOIN ADAUTOSER SS ON SS.IPCODPACI = G.IPCODPACI 
        AND SS.NUMINGRES = G.NUMINGRES 
        AND SS.CODSERIPS = G.CODSERIPS 
        AND SS.NUMEFOLIO = G.NUMEFOLIO
    WHERE G.CODCENATE = @CentroAtencion  
        AND G.ESTSERIPS IN (1,2,3,5)
    
    UNION ALL
    
    -- =============================================
    -- 5. PROCEDIMIENTOS NO QX / TERAPIAS (CON TABLA AUXILIAR)
    -- =============================================
    SELECT  
        I.UFUCODIGO,
        J.TIPSERIPS,
        CASE J.TIPSERTER 
            WHEN 1 THEN 'Terapias - Procedimientos no Qx' 
            ELSE 'Procedimientos no Qx' 
        END,
        I.IPCODPACI,
        I.NUMINGRES,
        I.FECORDMED,
        RTRIM(J.CODSERIPS),
        RTRIM(J.DESSERIPS),
        I.ESTSERIPS, 
        CASE J.TIPSERTER 
            WHEN 1 THEN 
                CASE I.ESTSERIPS 
                    WHEN 1 THEN 
                        CASE WHEN TR.CantidadRealizadas > 0 THEN 'Pendiente' ELSE 'Ordenado' END
                    WHEN 2 THEN 'Cumplido' 
                END
            ELSE 
                CASE I.ESTSERIPS 
                    WHEN 1 THEN 'Solicitado' 
                    WHEN 2 THEN 'Completado'  
                END  
        END,
        J.TIPSERIPS,
        I.NUMEFOLIO,
        I.ESTSERIPS,
        CASE 
            WHEN (S.SUSCEPTIB IS NULL OR S.SUSCEPTIB = 0 OR SS.PROESTADO IS NULL) THEN 'No requiere autorización' 
            WHEN SS.PROESTADO = 1 THEN 'Pendiente Solicitud'
            WHEN SS.PROESTADO = 2 THEN 'Solicitado'
            WHEN SS.PROESTADO = 3 THEN 'Autorizado'
            WHEN SS.PROESTADO = 4 THEN 'Anulado'
            WHEN SS.PROESTADO = 5 THEN 'No autorizado'
            ELSE 'Pendiente por Autorizar'
        END,
        I.CANSERIPS,
        ISNULL(TR.CantidadRealizadas, 0),
        I.AUTO,
        ISNULL(J.TIPSERTER, 0),
        '',
        '',
        CASE J.SERIPSPOS WHEN 1 THEN 'Si' ELSE 'NO' END
    FROM HCORDPRON I
    INNER JOIN INCUPSIPS J ON J.CODSERIPS = I.CODSERIPS 
    INNER JOIN @TablaIngresosFiltro TIF ON I.NUMINGRES = TIF.Ingreso
    LEFT JOIN ADCONFSER S ON S.CODSERIPS = I.CODSERIPS
    LEFT JOIN ADAUTOSER SS ON SS.IPCODPACI = I.IPCODPACI 
        AND SS.NUMINGRES = I.NUMINGRES 
        AND SS.CODSERIPS = I.CODSERIPS 
        AND SS.NUMEFOLIO = I.NUMEFOLIO
    LEFT JOIN @TerapiasRealizadas TR ON TR.IPCODPACI = I.IPCODPACI 
        AND TR.NUMINGRES = I.NUMINGRES 
        AND TR.CODSERIPS = I.CODSERIPS
    WHERE I.CODCENATE = @CentroAtencion  
        AND I.ESTSERIPS IN (1,2)  
        AND I.MANEXTPRO = 0 
    
    UNION ALL
    
    -- =============================================
    -- 6. REMISIONES
    -- =============================================
    SELECT 
        P.UFUCODIGO,
        99,
        'Remisión',
        P.IPCODPACI,
        P.NUMINGRES,
        P.FECSOLICIT,
        '',
        'Remisión',
        '',
        CASE P.ESTADO 
            WHEN 1 THEN 'Solicitado'  
            WHEN 2 THEN 'Pendiente o gestionando' 
            WHEN 3 THEN 'Aceptado con pendiente de salida' 
        END,
        0,
        0,
        CASE P.ESTADO 
            WHEN 1 THEN 0  
            WHEN 2 THEN 0 
            WHEN 3 THEN 4 
        END,
        'No aplica',
        0,
        0,
        P.AUTO,
        0,
        '',
        '',
        ''
    FROM HCREFCONP P
    INNER JOIN @TablaIngresosFiltro TIF ON P.NUMINGRES = TIF.Ingreso
    WHERE P.CODCENATE = @CentroAtencion  
        AND P.UFUCODIGO = @UFuncional  
        AND P.ESTADO IN (1,2,3);

    -- =============================================
    -- TABLAS AUXILIARES PARA SUBCONSULTAS DEL SELECT FINAL
    -- =============================================
    
    -- Población especial por paciente
    DECLARE @PoblacionEspecial AS TABLE(
        IPCODPACI VARCHAR(36) PRIMARY KEY,
        CantidadPobEspecial INT
    );

    INSERT INTO @PoblacionEspecial
    SELECT 
        P.Identificacion,
        COUNT(*)
    FROM #Pacientes P
    INNER JOIN dbo.ADPOBESPEPAC Z ON Z.IPCODPACI = P.Identificacion
    INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE
    WHERE TIPOPOESPERIES = 1
    GROUP BY P.Identificacion;

    -- Acompañantes por ingreso
    DECLARE @Acompanantes AS TABLE(
        NUMINGRES VARCHAR(36) PRIMARY KEY,
        CantidadAcompanantes INT
    );

    INSERT INTO @Acompanantes
    SELECT 
        P.Ingreso,
        COUNT(*)
    FROM #Pacientes P
    INNER JOIN ADACOMPAN AC ON AC.NUMINGRES = P.Ingreso
    GROUP BY P.Ingreso;

    -- =============================================
    -- SELECT FINAL OPTIMIZADO (Sin subconsultas correlacionadas)
    -- =============================================
    SELECT 
        N.CODTIPPAC AS TipoPaciente,
        L.CODICAMAS AS 'Codigo Cama',
        RTRIM(L.DESCCAMAS) AS Cama,
        S.FechaSolicitud,
        S.Identificacion,
        RTRIM(K.IPNOMCOMP) AS 'Nombre Completo',
        K.IPFECNACI AS 'Fecha Nacimiento',
        S.NombreTipoServicio,
        CAST('' AS CHAR(50)) AS Edad,
        CASE WHEN K.IPTIPODOC IN (6,7) THEN 1 ELSE 0 END AS ASMS,
        K.ZONAPARTADA,
        F.RIESGOAGRE,
        ISNULL(PE.CantidadPobEspecial, 0) AS POBESPECIAL,
        N.VIVESOLO,
        ISNULL(AC.CantidadAcompanantes, 0) AS ACOMPANANTES,
        CONVERT(BIT, 0) AS Riesgo,
        S.Ingreso,
        RTRIM(O.NOMENTIDA) AS EntidadPaciente,
        RTRIM(Q.NOMDIAGNO) AS Diagnostico,
        RTRIM(G.DESESPECI) AS DescripcionEspecialidad,
        RTRIM(S.CodigoServicio) AS 'Codigo Servicio',
        RTRIM(S.NombreServicio) AS 'Nombre Servicio',
        RTRIM(S.NombreEstado) AS 'Estado Servicio',
        S.CodigoTipoServicio,
        S.NumeroFolio,
        S.CodigoEstadoServicio,
        S.Autorizacion,
        S.EntityId,
        S.Cantidad,
        S.Cantidadrea,
        S.Terapia,
        S.CodigoMotivoMuestraNoConforme,
        S.DescripcionMotivoMuestraNoConforme,
        S.ServicioPBS
    FROM #Pacientes S
    INNER JOIN ADINGRESO N ON N.NUMINGRES = S.Ingreso
    INNER JOIN INUNIFUNC Z ON Z.UFUCODIGO = S.CodigoUnidadFuncional
    LEFT JOIN CHCAMASHO L ON L.CODICAMAS = N.CODCAMACT  
    LEFT JOIN CHREGESTA M ON M.CODICAMAS = L.CODICAMAS AND M.REGESTADO = 1 
    INNER JOIN INPACIENT K ON K.IPCODPACI = S.Identificacion
    LEFT JOIN INENTIDAD O ON O.CODENTIDA = K.CODENTIDA 
    LEFT JOIN INDIAGNOS Q ON Q.CODDIAGNO = N.CODDIAING
    LEFT JOIN INESPECIA G ON N.CODESPTRA = G.CODESPECI 
    LEFT JOIN dbo.ADACTIVID F ON F.CODACTIVI = K.codactivi
    LEFT JOIN @PoblacionEspecial PE ON PE.IPCODPACI = K.IPCODPACI
    LEFT JOIN @Acompanantes AC ON AC.NUMINGRES = N.NUMINGRES
    ORDER BY S.Identificacion;

END
