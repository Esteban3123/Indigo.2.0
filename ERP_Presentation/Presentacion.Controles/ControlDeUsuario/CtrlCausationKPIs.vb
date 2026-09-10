'***********************************************************************
' Assembly         : Presentation.Controls
' Author           : Andres Alarcon
' Created          : 2025-10-19
'
' Last Modified By : 
' Last Modified On : 
' Description      : Control de KPIs para Reconocimiento de Causaciones
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports DevExpress.XtraEditors

''' <summary>
''' Control para mostrar KPIs de Reconocimiento de Causaciones
''' </summary>
Public Class CtrlCausationKPIs
    Inherits XtraUserControl

#Region "Properties"

    Private _valorCostoReconocido As Decimal = 0
    Private _porcentajeReconocido As Decimal = 0
    Private _porcentajeFallidas As Decimal = 0
    Private _growthRate As Decimal = 0
    Private _ordenessinCausar As Integer = 0

    ''' <summary>
    ''' Valor total del costo reconocido
    ''' </summary>
    Public Property ValorCostoReconocido As Decimal
        Get
            Return _valorCostoReconocido
        End Get
        Set(value As Decimal)
            _valorCostoReconocido = value
            UpdateValorCostoReconocido()
        End Set
    End Property

    ''' <summary>
    ''' Porcentaje de causaciones reconocidas
    ''' </summary>
    Public Property PorcentajeReconocido As Decimal
        Get
            Return _porcentajeReconocido
        End Get
        Set(value As Decimal)
            _porcentajeReconocido = value
            UpdatePorcentajeReconocido()
        End Set
    End Property

    ''' <summary>
    ''' Porcentaje de causaciones que fallaron
    ''' </summary>
    Public Property PorcentajeFallidas As Decimal
        Get
            Return _porcentajeFallidas
        End Get
        Set(value As Decimal)
            _porcentajeFallidas = value
            UpdatePorcentajeFallidas()
        End Set
    End Property

    ''' <summary>
    ''' Tasa de crecimiento con respecto al mes anterior
    ''' </summary>
    Public Property GrowthRate As Decimal
        Get
            Return _growthRate
        End Get
        Set(value As Decimal)
            _growthRate = value
            UpdateGrowthRate()
        End Set
    End Property

    ''' <summary>
    ''' Cantidad de órdenes de servicio sin causar
    ''' </summary>
    Public Property OrdenesSinCausar As Integer
        Get
            Return _ordenessinCausar
        End Get
        Set(value As Integer)
            _ordenessinCausar = value
            UpdateOrdenesSinCausar()
        End Set
    End Property

#End Region

#Region "Methods"

    Private Sub UpdateValorCostoReconocido()
        If lblValorReconocido IsNot Nothing Then
            lblValorReconocido.Text = String.Format("${0:N0}", _valorCostoReconocido)
        End If
    End Sub

    Private Sub UpdatePorcentajeReconocido()
        If lblPorcentajeReconocido IsNot Nothing Then
            lblPorcentajeReconocido.Text = String.Format("{0:N2}%", _porcentajeReconocido)
            
            ' Actualizar barra de progreso
            If progressReconocido IsNot Nothing Then
                progressReconocido.EditValue = Convert.ToInt32(_porcentajeReconocido)
            End If
        End If
    End Sub

    Private Sub UpdatePorcentajeFallidas()
        If lblPorcentajeFallidas IsNot Nothing Then
            lblPorcentajeFallidas.Text = String.Format("{0:N2}%", _porcentajeFallidas)
            
            ' Actualizar barra de progreso
            If progressFallidas IsNot Nothing Then
                progressFallidas.EditValue = Convert.ToInt32(_porcentajeFallidas)
            End If
        End If
    End Sub

    Private Sub UpdateGrowthRate()
        If lblGrowthRate IsNot Nothing Then
            Dim signo As String = If(_growthRate >= 0, "+", "")
            lblGrowthRate.Text = String.Format("{0}{1:N2}%", signo, _growthRate)
            
            ' Cambiar color según sea positivo o negativo
            If _growthRate >= 0 Then
                lblGrowthRate.Appearance.ForeColor = Color.Green
            Else
                lblGrowthRate.Appearance.ForeColor = Color.Red
            End If
        End If
    End Sub

    Private Sub UpdateOrdenesSinCausar()
        If lblOrdenesSinCausar IsNot Nothing Then
            lblOrdenesSinCausar.Text = _ordenessinCausar.ToString("N0")
        End If
    End Sub

    ''' <summary>
    ''' Actualiza todos los KPIs de una vez
    ''' </summary>
    Public Sub ActualizarKPIs(valorReconocido As Decimal, porcentajeReconocido As Decimal,
                              porcentajeFallidas As Decimal, growthRate As Decimal,
                              ordenesSinCausar As Integer)
        Me.ValorCostoReconocido = valorReconocido
        Me.PorcentajeReconocido = porcentajeReconocido
        Me.PorcentajeFallidas = porcentajeFallidas
        Me.GrowthRate = growthRate
        Me.OrdenesSinCausar = ordenesSinCausar
    End Sub

    ''' <summary>
    ''' Limpia todos los KPIs
    ''' </summary>
    Public Sub LimpiarKPIs()
        Me.ValorCostoReconocido = 0
        Me.PorcentajeReconocido = 0
        Me.PorcentajeFallidas = 0
        Me.GrowthRate = 0
        Me.OrdenesSinCausar = 0
    End Sub

#End Region

End Class

