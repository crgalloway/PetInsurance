$(document).ready(function(){
    $('#ShowRegisterButton').click(function(){
        $('#RegisterForm').slideToggle();
        if($('#ShowRegisterButton').html() == "New? Click here to Register!")
        {
            $('#ShowRegisterButton').html("Already Registered? Just Login above!")
        }
        else{
            $('#ShowRegisterButton').html("New? Click here to Register!")
        }
    })
    $('#ShowNewPetButton').click(function(){
        $('#NewPetForm').slideToggle();
        if($('#ShowNewPetButton').html() == "Add a Pet!")
        {
            $('#ShowNewPetButton').html("Don't Add a Pet Right Now")
        }
        else{
            $('#ShowNewPetButton').html("Add a Pet!")
        }
    })
    $('#BreedSelect').change(function(){
        if($(this).val()==0)
        {
            $('#NewBreedForm').show();
        }
        else{
            $('#NewBreedForm').hide();
        }
    })
    
})