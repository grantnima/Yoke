script.type="text/javascript";  
script.src="jq/jquery-3.1.0.js";  
//购物车数量按钮
function cartsjia(kucun){
	var valus1=$('#carttext').val();
	var valus2=parseInt(valus1)+1;
	$('#carttext').val(valus2);
	$('#carttext1').val(valus2);
	if(valus2>1){
	$('.cartsj').removeAttr("disabled");		
	}
	if(valus2>=kucun){
	$('.cartsjia').attr("disabled", "disabled");
	$('#carttext').val(kucun);
	$('#carttext1').val(kucun);
	}
}
function cartsjian(kucun){	
	var valus1=$('#carttext').val();
	var valus2=parseInt(valus1)-1;
	$('#carttext').val(valus2);
	$('#carttext1').val(valus2);
	if(valus2<kucun){
	$('.cartsjia').removeAttr("disabled");	
	}
	if(valus2<=1){
	$('.cartsj').attr("disabled", "disabled");
		$('#carttext').val(1);
	$('#carttext1').val(1);
	}
}
//checkbox 全选
function checkall(checkbox,checkname){
	var allCheckBoxs=document.getElementsByName(checkname);
	if(document.getElementById(checkbox).checked){     
   //循环设置控件为选中状态
   for (var i=0;i<allCheckBoxs.length ;i++){
       if(allCheckBoxs[i].type=="checkbox"){
       	allCheckBoxs[i].checked=true;          
       }
    }  
	}
    else{
       for (var i=0;i<allCheckBoxs.length ;i++){
       	if(allCheckBoxs[i].type=="checkbox"){
       	allCheckBoxs[i].checked=false;      
       }
    }  
    }
}
//购物车改变
function cartextchange(textone,kucun){
	var textval=document.getElementById(textone).value;
	if(textval<1){
		alert("最低购买一件");
		document.getElementById(textone).value=1;
	}
	if(textval>kucun){
		alert("最多购买"+kucun+"件");
		document.getElementById(textone).value=1;
		$('.cartsjia').removeAttr("disabled");
	}
	if(textval>1){
		$('.cartsj').removeAttr("disabled");
	}
}
function usetextchange(){
		var val=$('#carttext').val();
	    var vals= /^[0-9]+$/;
	        if(val.search(vals)!=0){
	            alert("请输入正确的数值！")
	           	$('#carttext').val(1);
	        }               
}
//手机号验证
function phonetest(phone){
	    var reg=/^(13[0-9]{9}|15[89][0-9]{8})$/
	    if(phone.search(reg)){
	    	return 0;
	    }
	   else{
	    	return 1;
	   }
}
//密码验证
function passwordtest(password){
	var reg=/^[0-9]{6}$/
	if(password.search(reg)){
		return 0;
	}
	else{
	    return 1;
	}
}
//邮箱格式验证
function emailtest(email){
	var reg=/\w+@\w+\.\w/
	if(email.search(reg)){
		return 0;
	}
	else{
	    return 1;
	}
}
function phonetext1(text,textp){
	var val=document.getElementById(text).value;
	var turn=phonetest(val);
	if(turn==0){
	    	document.getElementById(textp).innerText="请输入正确的手机号！"
	    	document.getElementById(text).select();
	    }
	    else{
	    	document.getElementById(textp).innerText=""
	    }
}
function passwordtext1(text,textp){
	var val=document.getElementById(text).value;
	var turn=passwordtest(val);
	 if(text=='wordtext'){
	 	var lasttext=$('#passtext').val();
	 	if(val!=lasttext){
	 		document.getElementById(textp).innerText="两次输入的密码不一致，请重新输入！"
	 	}
	 	else{
	 		document.getElementById(textp).innerText="";
	 	}
	 }
	 else{
	 	if(turn==0){
	    	document.getElementById(textp).innerText="请输入正确的纯6位数字密码！"
	    	document.getElementById(text).select();
	    }
	    else{
	    	document.getElementById(textp).innerText="";
	    }
	 }
	 if(text=='passtext'){
	 	$('#wordtext').val('');
	 }
}
function emailtext1(){
	var val=$('#emailtext').val();
	var turn=emailtest(val)
	if(turn==0){
		$('#emailp').html('请输入正确的邮箱！');
	}
	else{
		$('#emailp').html('');
	}
}
