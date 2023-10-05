# How to run project
muốn chạy cả project thì phải chayj **BE** trước sau đó chạy **FE**
## Backend
1. chuột phải vào project `IMS.InfraStructure` chọn `Open in Terminal`
2. gõ `dotnet ef database update` - cái này chạy lúc đầu tiên khi chưa có database trong máy, hoặc update database (các lần sau bỏ qua)
3. bấm nút run `visual stdio` như bình thường - hiện thị ra swagger
## Frontend
1. install npm run `npm install`
2. run `ng serve`
3. localhost `localhost://4200` 



# Angular

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 16.1.7.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.
