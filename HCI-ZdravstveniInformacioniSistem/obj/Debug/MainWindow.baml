   M S B A M L   `   `   `  ????\  XHCI-ZdravstveniInformacioniSistem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:HCI_ZdravstveniInformacioniSistem!HCI_ZdravstveniInformacioniSistem  5   	   q=clr-namespace:HCI_ZdravstveniInformacioniSistem.user_controls/HCI_ZdravstveniInformacioniSistem.user_controls  5   	   2    ,HCI_ZdravstveniInformacioniSistem.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:HCI_ZdravstveniInformacioniSistem     5   	   Wuser_controls=clr-namespace:HCI_ZdravstveniInformacioniSistem.user_controls     5   	   -   5          ?Name$  LoginWindow??5   	    ? Title$% Zdravstveni informacioni sistem??5	   	   $	??460??61   $	??800??6>    ? UseLayoutRounding .  ? 
ResizeMode$ NoResize=?6c    ? Icon$ 
/icon2.png??6y   .??5
      ? -   5         ?Name$ 	main_grid??5
      ??5   
   ?? 5       ?? ImageSource$ /resources/login.jpg??6    j? TileMode$
 None=?6<    ?? Opacity$ 0.995Q?6L   5      	 ? ColumnDefinitions	 5   
   ?? 5      $
??47.4 ?6   ?? 5      $??213.6 ?6   6/   ?? 5      $	??540 ?6   6-   
 ? RowDefinitions
 5   
   ?? 5      $
??22.4 ?6   ?? 5      $	??236 ?6   6+   ?? 5      $??159.04 ?6   6.   .+?5   
   ?   9HCI_ZdravstveniInformacioniSistem.user_controls.LoginForm  -   5          Name$ CredentialsInputForm??5   #   $??1??6A   $??Center=?6Q   $??Center=?6l   $??1??6?   $	??200??6?   $	??200??6?   ?? 5   
   -   5         ??Name$ DoctorSignIn_btn??5   Y   $??Center=?6      CustomButton#??[  63   $??0,0,0,50q?6s   $??Prijava??6?   $??Center=?6?   $??Bottom=?6?    J? SnapsToDevicePixels .  ? ToolTip$) #Opcija za prijavu ljekara na sistem??6?   $??1??6  $??1??6C  $??1??6P  ?? 5   
   $??1??6   $??Center=?6    $??Center=?6D   $??88??6a   $??2??6m   $	??ZIS??6z   $	??Top=?6?   $	??200??6?   $??72(?6?   ???	#FFD63C3C$??0,10,0,0q?6?   $??Cooper Black)?6   J? RenderTransformOrigin$ 0.556,0.888&?6*  ??6?   Z? ??6?   Z? 6*  	? 5   
   -   5        $	??SRP??5      $
??Left=?6$   $??	4,145,0,0q?6?   $??2??6R   $	??Top=?6_    d? 	IsChecked$
 TrueY?6w   $
??Bold"?6?   	? 5   
   -   5        $	??ENG??5      $
??Left=?6$   $??	4,125,0,0q?6?   $??2??6R   $	??Top=?6_   $??2??6w   $
??Bold"?6?   5       5!      