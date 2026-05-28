# Лабораторная работа №11
Тема работы: Навигация в MVVM-приложениях. Подход ViewModel-First

Цель работы: изучить механизмы навигации между экранами в MVVM-приложении без использования стандартных средств WPF (Frame/Page).

В WPF существует несколько подходов организации навигации:
<img width="829" height="295" alt="image" src="https://github.com/user-attachments/assets/6df0f2f5-71e5-4a82-afa4-fcb23bc80712" />
Наиболее «чистым» с точки зрения MVVM является связка ContentControl + DataTemplate, так как она:
  - Не создаёт жёстких связей между View и ViewModel.
  - Полностью поддерживает внедрение зависимостей (DI).
  - Не требует подключения сторонних фреймворков (Prism, RegionManager и т.д.).
ViewModel-First навигация подразумевает, что приложение управляется состоянием ViewModel. Мы создаём экземпляр VM через DI-контейнер, а WPF автоматически находит соответствующий визуальный шаблон (DataTemplate) и отображает его внутри ContentControl. Главное окно выступает в роли Shell – контейнера с меню навигации и областью контента.

<img width="541" height="58" alt="image" src="https://github.com/user-attachments/assets/b07735fa-b3c8-4180-b398-f010601e2692" />
MainViewModel управляет переходами через ShowContactsCommand и ShowAboutCommand. При инициализации автоматически открывается экран контактов.


В App.xaml определены DataTemplate для автоматического маппинга:
<img width="470" height="233" alt="image" src="https://github.com/user-attachments/assets/ed2d8a73-2dfe-47d8-ab4f-4bb055a7b6c8" />
