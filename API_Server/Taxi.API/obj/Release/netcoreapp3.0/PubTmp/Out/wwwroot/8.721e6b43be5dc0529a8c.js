(window.webpackJsonp=window.webpackJsonp||[]).push([[8],{g7DB:function(n,t,o){"use strict";o.r(t),o.d(t,"LoginModule",(function(){return M}));var e=o("PCNd"),i=o("ofXK"),a=function(n){return n.ADMIN="admin",n.CUSTOMER="customer",n.DRIVER="driver",n}({}),r=o("fXoL"),c=o("PVyW"),s=o("tyNb");let l=(()=>{class n{constructor(n,t){this.authService=n,this.router=t}canActivate(n,t){return this.checkUserLogin(n,t.url)}canActivateChild(n,t){return this.canActivate(n,t)}canDeactivate(n,t,o,e){return!0}canLoad(n,t){return!0}checkUserLogin(n,t){if(this.authService.isLoggedIn()){const t=this.authService.getRole();return!(!n.data.role||n.data.role!==t)||(this.router.navigate([""]),!1)}return this.router.navigate([""]),!1}}return n.\u0275fac=function(t){return new(t||n)(r.Wb(c.a),r.Wb(s.b))},n.\u0275prov=r.Ib({token:n,factory:n.\u0275fac,providedIn:"root"}),n})();var d=o("3Pt+"),g=o("lNOl"),p=o("0IaG"),u=o("Kcz0");function b(n,t){1&n&&(r.Sb(0,"span"),r.Bc(1,"Please input Username"),r.Rb())}function f(n,t){if(1&n&&(r.Sb(0,"div",23),r.zc(1,b,2,0,"span",24),r.Rb()),2&n){const n=r.dc();r.Bb(1),r.jc("ngIf",n.f.username.errors.required)}}function h(n,t){1&n&&(r.Sb(0,"span"),r.Bc(1,"Please input Password"),r.Rb())}function m(n,t){if(1&n&&(r.Sb(0,"div",23),r.zc(1,h,2,0,"span",24),r.Rb()),2&n){const n=r.dc();r.Bb(1),r.jc("ngIf",n.f.password.errors.required)}}const x=function(n){return{small_label:n}},_=[{path:"",component:(()=>{class n{constructor(n,t,o,e,i){this.formBuilder=n,this.route=t,this.router=o,this.authenticationService=e,this.dialog=i,this.loading=!1,this.submitted=!1,this.error=""}ngOnInit(){this.loginForm=this.formBuilder.group({username:["",d.z.required],password:["",d.z.required]})}get f(){return this.loginForm.controls}onSubmit(){if(this.submitted=!0,this.loginForm.invalid)return void console.log("fail");const n=this.dialog.open(g.a,{width:"380px",height:"230px",data:{isLoading:!0,isSuccess:!1,isFailed:!1,loadingMessage:"Logging in..."}});this.authenticationService.login(this.f.username.value,this.f.password.value).pipe().subscribe(t=>{n.close(),this.router.navigate([this.authenticationService.getRole()],{relativeTo:this.route}),console.log(t)},t=>{n.close(),this.dialog.open(g.a,{width:"380px",height:"180px",data:{isLoading:!1,isSuccess:!1,isFailed:!0,failedMessage:"Please check your username and password"}}),console.log(t),this.error=t})}logout(){this.authenticationService.logout()}}return n.\u0275fac=function(t){return new(t||n)(r.Mb(d.f),r.Mb(s.a),r.Mb(s.b),r.Mb(c.a),r.Mb(p.b))},n.\u0275cmp=r.Gb({type:n,selectors:[["app-login"]],decls:36,vars:10,consts:[[1,"formContainer"],[1,"login_form",3,"formGroup","ngSubmit"],[1,"form_logo",3,"taxiColor"],[1,"inputBigContainer"],[1,"inputContainer"],[1,"form_icon","fas","fa-envelope"],["type","text","required","","formControlName","username",1,"login_input"],["email",""],[3,"ngClass"],[1,"focus-border"],["class","invalidContainer",4,"ngIf"],[1,"form_icon","fas","fa-lock"],["type","password","required","","formControlName","password",1,"login_input"],["password",""],["type","submit",1,"header_button"],[1,"icon_social_contain"],["target","_blank",1,"shadow_hover","google"],[1,"fab","fa-google"],["target","_blank",1,"shadow_hover","face"],[1,"fab","fa-facebook-square"],["target","_blank",1,"shadow_hover","twitter"],[1,"fab","fa-twitter"],["routerLink","/register",1,"registerLink"],[1,"invalidContainer"],[4,"ngIf"]],template:function(n,t){if(1&n&&(r.Sb(0,"div",0),r.Sb(1,"form",1),r.Zb("ngSubmit",(function(){return t.onSubmit()})),r.Sb(2,"h1"),r.Bc(3,"Welcome"),r.Rb(),r.Nb(4,"app-app-logo",2),r.Sb(5,"div",3),r.Sb(6,"div",4),r.Nb(7,"i",5),r.Nb(8,"input",6,7),r.Sb(10,"label",8),r.Bc(11,"Email"),r.Rb(),r.Nb(12,"span",9),r.Rb(),r.zc(13,f,2,1,"div",10),r.Rb(),r.Sb(14,"div",3),r.Sb(15,"div",4),r.Nb(16,"i",11),r.Nb(17,"input",12,13),r.Sb(19,"label",8),r.Bc(20,"Password"),r.Rb(),r.Nb(21,"span",9),r.Rb(),r.zc(22,m,2,1,"div",10),r.Rb(),r.Sb(23,"button",14),r.Bc(24," Login "),r.Rb(),r.Sb(25,"h2"),r.Bc(26,"Or sign in with"),r.Rb(),r.Sb(27,"div",15),r.Sb(28,"a",16),r.Nb(29,"i",17),r.Rb(),r.Sb(30,"a",18),r.Nb(31,"i",19),r.Rb(),r.Sb(32,"a",20),r.Nb(33,"i",21),r.Rb(),r.Rb(),r.Sb(34,"a",22),r.Bc(35,"Sign up if you dont have an account"),r.Rb(),r.Rb(),r.Rb()),2&n){const n=r.pc(9),o=r.pc(18);r.Bb(1),r.jc("formGroup",t.loginForm),r.Bb(3),r.jc("taxiColor","black"),r.Bb(6),r.jc("ngClass",r.nc(6,x,n.value)),r.Bb(3),r.jc("ngIf",t.submitted&&t.f.username.errors),r.Bb(6),r.jc("ngClass",r.nc(8,x,o.value)),r.Bb(3),r.jc("ngIf",t.submitted&&t.f.password.errors)}},directives:[d.B,d.r,d.j,u.a,d.d,d.x,d.q,d.h,i.k,i.m,s.e],styles:['.formContainer[_ngcontent-%COMP%]{display:flex;width:100vw;height:100vh;margin:0;padding:0;justify-content:center;background-image:url(DriverBackground.a9afc26232486876e67a.jpg);background-repeat:no-repeat;background-size:cover}.login_form[_ngcontent-%COMP%]{display:flex;flex-direction:column;width:30%;height:580px;align-items:center;box-shadow:0 0 10px 4px rgba(115,65,180,.1);border-radius:10px;margin-top:6%;padding-top:38px;background-color:#fff}.form_logo[_ngcontent-%COMP%]{padding-right:5%;margin-bottom:9%}.login_form[_ngcontent-%COMP%]   h1[_ngcontent-%COMP%]{font-size:30px;font-weight:600;letter-spacing:3px}.inputContainer[_ngcontent-%COMP%]{position:relative;margin-bottom:20px}[_ngcontent-%COMP%]:focus{outline:none}.form_icon[_ngcontent-%COMP%]{position:absolute;left:-20%;top:15%;font-size:25px;color:rgba(75,60,88,.815)}input[_ngcontent-%COMP%]{font:17px Lato,Arial,sans-serif;color:#333;width:100%;box-sizing:border-box;letter-spacing:1px}.login_input[_ngcontent-%COMP%]{border:0;padding:4px 0;border-bottom:1px solid #ccc;background-color:initial;position:relative;z-index:2}.login_input[_ngcontent-%COMP%] ~ .focus-border[_ngcontent-%COMP%]{position:absolute;bottom:0;left:50%;width:0;height:2px;background-color:#b386ec;transition:.4s}.login_input[_ngcontent-%COMP%]:focus ~ .focus-border[_ngcontent-%COMP%]{width:100%;transition:.4s;left:0}.login_input[_ngcontent-%COMP%] ~ label[_ngcontent-%COMP%]{position:absolute;font-size:16px;left:0;width:100%;top:9px;color:#aaa;transition:.3s;z-index:1;letter-spacing:.5px}.login_input[_ngcontent-%COMP%]:focus ~ label[_ngcontent-%COMP%], .login_input[_ngcontent-%COMP%] ~ .small_label[_ngcontent-%COMP%]{top:-16px;font-size:12px;color:#b386ec;transition:.3s}.header_button[_ngcontent-%COMP%]{font-size:15px;z-index:1;position:relative;color:#fff;padding:8px 27px;border-radius:8px;background:transparent;overflow:hidden;border:none;margin-top:7%;cursor:pointer}.header_button[_ngcontent-%COMP%]:before{z-index:-1;content:"";position:absolute;width:300%;height:131%;top:-14%;left:-129%;background:-webkit-linear-gradient(right,#21d4fd,#b721ff,#21d4fd,#b721ff);transition:all .3s ease-in-out}.header_button[_ngcontent-%COMP%]:focus{outline:none}.header_button[_ngcontent-%COMP%]:hover:before{left:-8%}.login_form[_ngcontent-%COMP%]   h2[_ngcontent-%COMP%]{margin-top:2%}.shadow_hover[_ngcontent-%COMP%]{display:inline-block;position:relative;transition-duration:.3s;transition-property:transform;-webkit-tap-highlight-color:rgba(0,0,0,0);transform:translateZ(0);box-shadow:0 0 1px transparent;color:#3b3a3b}.shadow_hover[_ngcontent-%COMP%]:before{pointer-events:none;position:absolute;z-index:-1;content:"";top:85%;left:5%;height:10px;width:90%;opacity:0;background:radial-gradient(ellipse at center,rgba(0,0,0,.69) 0,transparent 80%);transition-duration:.3s}.shadow_hover[_ngcontent-%COMP%]:hover{transform:translateY(-6px);animation-name:hover;animation-duration:1.5s;animation-delay:.3s;animation-timing-function:linear;animation-iteration-count:infinite;animation-direction:alternate}.shadow_hover[_ngcontent-%COMP%]:hover:before{opacity:.4;transform:translateY(6px);animation-name:hover-shadow;animation-duration:1.5s;animation-delay:.3s;animation-timing-function:linear;animation-iteration-count:infinite;animation-direction:alternate}.face[_ngcontent-%COMP%]:hover{color:#4267b2}.twitter[_ngcontent-%COMP%]:hover{color:#1da1f2}.google[_ngcontent-%COMP%]:hover{color:red}.icon_social_contain[_ngcontent-%COMP%]   a[_ngcontent-%COMP%]:hover{cursor:pointer;transform:translateY(-5px)}.icon_social_contain[_ngcontent-%COMP%]   a[_ngcontent-%COMP%]{transition:all .3s}.icon_social_contain[_ngcontent-%COMP%]{display:flex;width:116px;height:20px;justify-content:space-between;font-size:23px}@keyframes hover-shadow{0%{transform:translateY(6px);opacity:.4}50%{transform:translateY(3px);opacity:1}to{transform:translateY(6px);opacity:.4}}@keyframes hover{50%{transform:translateY(-2px)}to{transform:translateY(-7px)}}.registerLink[_ngcontent-%COMP%]{font-size:15px;text-decoration:none;margin-top:15px}.inputBigContainer[_ngcontent-%COMP%]{display:flex;flex-direction:column;width:60%;align-items:center;position:relative;margin-bottom:10px}.invalidContainer[_ngcontent-%COMP%]{position:absolute;width:100%;top:31px;left:15%}.invalidContainer[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]{color:#d82222;margin-right:10px}']}),n})()},{path:"admin",loadChildren:()=>Promise.all([o.e(2),o.e(0),o.e(7)]).then(o.bind(null,"13Ib")).then(n=>n.AdminModule),canActivate:[l],data:{role:a.ADMIN}},{path:"driver",loadChildren:()=>Promise.all([o.e(2),o.e(3),o.e(0),o.e(12)]).then(o.bind(null,"LYxu")).then(n=>n.DriverModule),canActivate:[l],data:{role:a.DRIVER}},{path:"customer",loadChildren:()=>Promise.all([o.e(2),o.e(3),o.e(0),o.e(11)]).then(o.bind(null,"R1GA")).then(n=>n.CustomerModule),canActivate:[l],data:{role:a.CUSTOMER}}];let C=(()=>{class n{}return n.\u0275mod=r.Kb({type:n}),n.\u0275inj=r.Jb({factory:function(t){return new(t||n)},imports:[[s.f.forChild(_)],s.f]}),n})(),M=(()=>{class n{}return n.\u0275mod=r.Kb({type:n}),n.\u0275inj=r.Jb({factory:function(t){return new(t||n)},imports:[[i.c,C,e.a,d.k,d.w]]}),n})()},lNOl:function(n,t,o){"use strict";o.d(t,"a",(function(){return g}));var e=o("0IaG"),i=o("fXoL"),a=o("ofXK"),r=o("Xa2L");function c(n,t){if(1&n&&(i.Sb(0,"div",3),i.Sb(1,"div",4),i.Sb(2,"div",5),i.Nb(3,"span",6),i.Nb(4,"span",7),i.Nb(5,"div",8),i.Nb(6,"div",9),i.Rb(),i.Rb(),i.Sb(7,"h3"),i.Bc(8),i.Rb(),i.Rb()),2&n){const n=i.dc();i.Bb(8),i.Cc(n.data.successMessage)}}function s(n,t){if(1&n&&(i.Sb(0,"div",10),i.Nb(1,"mat-spinner"),i.Sb(2,"h3"),i.Bc(3),i.Rb(),i.Rb()),2&n){const n=i.dc();i.Bb(3),i.Cc(n.data.loadingMessage)}}function l(n,t){if(1&n&&(i.Sb(0,"div",10),i.Nb(1,"i",11),i.Sb(2,"h3"),i.Bc(3),i.Rb(),i.Rb()),2&n){const n=i.dc();i.Bb(3),i.Cc(n.data.failedMessage)}}function d(n,t){1&n&&(i.Sb(0,"div"),i.Bc(1," you already rate this order\n"),i.Rb())}let g=(()=>{class n{constructor(n,t){this.dialogRef=n,this.data=t}ngOnInit(){}onNoClick(){this.dialogRef.close()}onYesClick(){return!0}}return n.\u0275fac=function(t){return new(t||n)(i.Mb(e.f),i.Mb(e.a))},n.\u0275cmp=i.Gb({type:n,selectors:[["app-inform-dialog"]],decls:4,vars:4,consts:[["class","successDialog",4,"ngIf"],["class","loadingDialog",4,"ngIf"],[4,"ngIf"],[1,"successDialog"],[1,"success-checkmark"],[1,"check-icon"],[1,"icon-line","line-tip"],[1,"icon-line","line-long"],[1,"icon-circle"],[1,"icon-fix"],[1,"loadingDialog"],[1,"fas","fa-times"]],template:function(n,t){1&n&&(i.zc(0,c,9,1,"div",0),i.zc(1,s,4,1,"div",1),i.zc(2,l,4,1,"div",1),i.zc(3,d,2,0,"div",2)),2&n&&(i.jc("ngIf",t.data.isSuccess),i.Bb(1),i.jc("ngIf",t.data.isLoading),i.Bb(1),i.jc("ngIf",t.data.isFailed),i.Bb(1),i.jc("ngIf",t.data.isAlreadyRate))},directives:[a.m,r.b],styles:['.success-checkmark[_ngcontent-%COMP%]{width:80px;height:115px;margin:0 auto}.success-checkmark[_ngcontent-%COMP%]   .check-icon[_ngcontent-%COMP%]{width:80px;height:80px;position:relative;border-radius:50%;box-sizing:initial;border:4px solid #4caf50}.success-checkmark[_ngcontent-%COMP%]   .check-icon[_ngcontent-%COMP%]:before{top:3px;left:-2px;width:30px;transform-origin:100% 50%;border-radius:100px 0 0 100px}.success-checkmark[_ngcontent-%COMP%]   .check-icon[_ngcontent-%COMP%]:after{top:0;left:30px;width:60px;transform-origin:0 50%;border-radius:0 100px 100px 0;animation:rotate-circle 4.25s ease-in}.success-checkmark[_ngcontent-%COMP%]   .check-icon[_ngcontent-%COMP%]:after, .success-checkmark[_ngcontent-%COMP%]   .check-icon[_ngcontent-%COMP%]:before{content:"";height:100px;position:absolute;background:#fff;transform:rotate(-45deg)}.success-checkmark[_ngcontent-%COMP%]   .check-icon[_ngcontent-%COMP%]   .icon-line[_ngcontent-%COMP%]{height:5px;background-color:#4caf50;display:block;border-radius:2px;position:absolute;z-index:10}.success-checkmark[_ngcontent-%COMP%]   .check-icon[_ngcontent-%COMP%]   .icon-line.line-tip[_ngcontent-%COMP%]{top:46px;left:14px;width:25px;transform:rotate(45deg);animation:icon-line-tip .75s}.success-checkmark[_ngcontent-%COMP%]   .check-icon[_ngcontent-%COMP%]   .icon-line.line-long[_ngcontent-%COMP%]{top:38px;right:8px;width:47px;transform:rotate(-45deg);animation:icon-line-long .75s}.success-checkmark[_ngcontent-%COMP%]   .check-icon[_ngcontent-%COMP%]   .icon-circle[_ngcontent-%COMP%]{top:-4px;left:-4px;z-index:10;width:80px;height:80px;border-radius:50%;position:absolute;box-sizing:initial;border:4px solid rgba(76,175,80,.5)}.success-checkmark[_ngcontent-%COMP%]   .check-icon[_ngcontent-%COMP%]   .icon-fix[_ngcontent-%COMP%]{top:8px;width:5px;left:26px;z-index:1;height:85px;position:absolute;transform:rotate(-45deg);background-color:#fff}@keyframes rotate-circle{0%{transform:rotate(-45deg)}5%{transform:rotate(-45deg)}12%{transform:rotate(-405deg)}to{transform:rotate(-405deg)}}@keyframes icon-line-tip{0%{width:0;left:1px;top:19px}54%{width:0;left:1px;top:19px}70%{width:50px;left:-8px;top:37px}84%{width:17px;left:21px;top:48px}to{width:25px;left:14px;top:45px}}@keyframes icon-line-long{0%{width:0;right:46px;top:54px}65%{width:0;right:46px;top:54px}84%{width:55px;right:0;top:35px}to{width:47px;right:8px;top:38px}}.loadingDialog[_ngcontent-%COMP%]{width:100%;display:flex;justify-content:center;align-items:center;flex-direction:column;padding-top:20px}.loadingDialog[_ngcontent-%COMP%]   h3[_ngcontent-%COMP%]{margin:10px 0 0}.loadingDialog[_ngcontent-%COMP%]   .fas[_ngcontent-%COMP%]{font-size:35px;color:red}.successDialog[_ngcontent-%COMP%]{display:flex;flex-direction:column;justify-content:center;align-items:center}']}),n})()}}]);