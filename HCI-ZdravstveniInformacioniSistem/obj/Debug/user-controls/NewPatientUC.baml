   M S B A M L   `   `   `  ????\  XHCI-ZdravstveniInformacioniSistem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:HCI_ZdravstveniInformacioniSistem.user_controls/HCI_ZdravstveniInformacioniSistem.user_controls  5      B    <HCI_ZdravstveniInformacioniSistem.user_controls.NewPatientUC   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Olocal=clr-namespace:HCI_ZdravstveniInformacioniSistem.user_controls     5      .??5	      ?   ? ColumnDefinitions  5
   
   ?? 5      $	??800 ?6   6-   ?? 5      $??* ?6   6+    ? RowDefinitions 5   
   ?? 5      $	??220 ?6   6+   ?? 5      $??* ?6   6)   .+?5   
   ?? -   5         ??Name$
 Name??5      $
??Left=?6    $??Center=?6B   $
??Left=?6d   $??30??6   $??	40,30,0,0q?6?    ?? TextWrapping$
 Wrap=?6?   $	??Top=?6?   $	??350??6?   6?   ?? 5   
   -   5        $
 Desc??5      $
??Left=?6    $??Center=?6B   $
??Left=?6d   $??
434,30,0,0q?6   $
 Wrap=?6?   $	??Top=?6?   $	??350??6?    ?? AcceptsReturn . $??27??6  ?? 5   
   -   5         ??Name$ SaveBtn??5         CustomButton#??[  6"   $
??Left=?6x   $??34??6?   $??	549,0,0,0q?6?   $??Center=?6?   $	??102??6?   ??6H   ?? *SaveBtnContent+ ?? Mode$ OneWay=?6?   ?? 5   
   $
??Left=?6:   $??38??6U   $??434,137,0,0q?6a   $	??Top=?6v   $	??350??6?   
???  ??$??15(?6?   $
??Bold"?6?   $??Center=?6?   ??6   ?? *
Feedback+$ OneWay=?6?   ?? 5   
   -   5        $	 Tel??5      $
??Left=?6   $??Center=?6A   $
??Left=?6c   $??	40,90,0,0q?6~   $
 Wrap=?6?   $	??Top=?6?   $	??350??6?    . $??27??6  (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ 
DatePicker??5      $
??Left=?6)   $??30??6D   $??143,141,0,0q?6P   $	??Top=?6e   $	??144??6}   $
??Bold"?6?   $??13(?6?      FirstDayOfWeek$ Monday=?6?   	   DisplayDate$	 
11.25.2022??6?   ??6?   Z? 6?   ?? 5   
   -   5        
 ??Name$
 HideLbl??5      $
??Left=?6Q   $??20??6l   $??143,144,0,0q?6x   $	??Top=?6?   $	??116??6?   
???????$??11(?6?   ??6!   ?? *HideLblContent+$ OneWay=?6?   5      5      