Imports  Domain.Entities
Imports Domain.Base.Entities

Public Interface IGeneralconciliation

    ''' <summary>
    ''' Funcion para obtener una lista de movimientos
    ''' </summary>
    ''' <returns></returns>
    Function saveMov(ByVal listmov As List(Of GlosaMovementGlosa)) As Task(Of ActionResult)

End Interface
