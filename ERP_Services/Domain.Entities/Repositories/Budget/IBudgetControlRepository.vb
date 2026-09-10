'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Jhossept Kevin Garay
' Created          : 21-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Public Interface IBudgetControlRepository
    Inherits IRepository(Of BudgetControl)

    ''' <summary>
    ''' Obtiene un documento que este en la tabla de control
    ''' </summary>
    ''' <param name="Consecutive">consecutivo del registro</param>
    ''' <param name="Process">tipo de proceso del documento......(MODIFICACION AL PTO = 1,TRASLADO AL PTO = 2,MODIFICACION AL PAC =3,TRASLADO AL PAC = 4,RECONOCIMIENTO = 5,MODIFICACION AL RECONOCIMIENTO = 6,RECAUDO = 7,MODIFICACION AL RECAUDO = 8,DISPONIBILIDAD = 9,MODIFICACION A LA DISPONIBILIDAD = 10,COMPROMISO = 11,MODIFICACION AL COMPROMISO = 12,PRORROGA DE DISPONIBILIDADES = 13,OBLIGACION = 14,MODIFICACION A LA OBLIGACION = 15,ORDEN DE PAGO = 16,MODIFICACION A LA ORDEN DE PAGO = 17,LIBERACION DE RECURSOS = 18,REINTEGRO = 19,RESERVA = 21,CUENTA POR PAGAR =22,DISPONIBILIDAD DE VFT = 23,COMPROMISO DE VFT = 24,OBLIGACION DE VFT = 25,ORDEN DE PAGO DE VFT = 26,SUSPENCION PRESUPUESTAL = 27,LEVANTAMIENTO PRESUPUESTAL = 28,CUENTAS POR COBRAR = 29,PRORROGA DE DOCUMENTOS = 30)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBudgetControl(Consecutive As String, Process As Integer) As BudgetControl

End Interface